using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Combat.Fighter))]
    [RequireComponent(typeof(Controls))]
    [RequireComponent(typeof(GameLog))]
    [RequireComponent(typeof(TopDownMovement))]
    [RequireComponent(typeof(Inventory.InventoryManager))]
    [RequireComponent(typeof(Equipment.EquipmentManager))]
    public class PlayerController : MonoBehaviour, Game.ISaveable
    {
        static public PlayerController instance;

        private Combat.Fighter fighter;

        private Inventory.InventoryManager inventory;
        private Equipment.EquipmentManager equipment;

        private GameLog gameLog;
        private Controls controls;

        private TopDownMovement topDownMovement;

        public Combat.Fighter Fighter { get => fighter; private set { } }
        public Inventory.InventoryManager Inventory { get => inventory; private set { } }
        public Equipment.EquipmentManager Equipment { get => equipment; private set { } }
        public TopDownMovement Movement { get => topDownMovement; private set { } }

        public delegate void OnInteract();
        public OnInteract onInteract;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(this.gameObject);

            fighter = GetComponent<Combat.Fighter>();
            fighter.Health.onChange += OnHealthChange;
            fighter.onHitByAttack += OnHitByAttack;
            fighter.onKilledByAttack += OnKilledByAttack;
            fighter.onAttackHasHit += OnAttackHasHit;
            fighter.onAttackHasKilled += OnAttackHasKilled;

            if (fighter.RangedWeapon != null)
            {
                fighter.RangedWeapon.onShoot += OnRangedShoot;
            }
            if (fighter.MeleeWeapon != null)
            {
                fighter.MeleeWeapon.onAttack += OnMeleeAttack; 
            }

            inventory = GetComponent<Inventory.InventoryManager>();
            equipment = GetComponent<Equipment.EquipmentManager>();

            inventory.onAddItem += OnAddInventoryItem;
            inventory.onRemoveItem += OnRemoveInventoryItem;

            equipment.onEquip += OnEquipItem;
            equipment.onUnequip += OnUnequipItem;

            topDownMovement = GetComponent<TopDownMovement>();

            topDownMovement.onMove += OnMove;
            topDownMovement.onJump += OnJump;

            gameLog = GetComponent<GameLog>();
            controls = GetComponent<Controls>();
        }

        private void OnDestroy()
        {
            if (fighter != null )
            {
                fighter.Health.onChange -= OnHealthChange;

                fighter.onHitByAttack -= OnHitByAttack;
                fighter.onKilledByAttack -= OnKilledByAttack;
                fighter.onAttackHasHit -= OnAttackHasHit;
                fighter.onAttackHasKilled -= OnAttackHasKilled;

                if (fighter.RangedWeapon != null)
                {
                    fighter.RangedWeapon.onShoot -= OnRangedShoot;
                }
                if (fighter.MeleeWeapon != null)
                { 
                    fighter.MeleeWeapon.onAttack -= OnMeleeAttack;
                }
            }
            if (inventory != null)
            {
                inventory.onAddItem -= OnAddInventoryItem;
                inventory.onRemoveItem -= OnRemoveInventoryItem;
            }
            if (equipment != null)
            {
                equipment.onEquip -= OnEquipItem;
                equipment.onUnequip -= OnUnequipItem;
            }
            if (topDownMovement != null)
            {
                topDownMovement.onMove -= OnMove;
                topDownMovement.onJump -= OnJump;
            }
        }

        private void Update()
        {
            controls.HandleInput();

            if (controls.Jump)
            {
                Movement.Jump();
            }

            if (controls.Interact && onInteract != null)
            {
                onInteract.Invoke();
                return;
            }

            if (controls.Attack && fighter.MeleeWeapon != null)
            {
                fighter.MeleeWeapon.Attack();
            }
            if (controls.Shoot && fighter.RangedWeapon != null)
            {
                fighter.RangedWeapon.Shoot();
            }
        }

        private void FixedUpdate()
        {
            if (controls.IsMoving)
            {
                Movement.Move(new Vector3(controls.Horizontal, 0f, controls.Vertical).normalized);
            }
        }

        /**
         * When fighter.health changes.
         */
        private void OnHealthChange(int delta, int currentHealth)
        {
            // @todo UI, Sound and vfx
        }

        /**
         * When the player is moving.
         */
        private void OnMove(Vector3 movement)
        {
            gameLog.LogMovement(movement);
        }

        /**
         * When the player is jumping.
         */
        private void OnJump(int jumpNum)
        {
            gameLog.LogJump(jumpNum);
        }

        /**
         * When the player is dashing.
         */
        private void OnDash()
        {
            gameLog.LogDash();
        }

        /**
         * When a battle results in the player getting hit.
         */
        private void OnHitByAttack(Combat.Result result)
        {
            return;
        }

        /**
         * When a battle results in the player getting killed.
         */
        private void OnKilledByAttack(Combat.Result result)
        {
            // @todo Handle death
            return;
        }

        /**
         * When a battle results in the player hitting an enemy.
         */
        private void OnAttackHasHit(Combat.Result result)
        {
            return;
        }

        /**
         * When a battle results in the player killing an enemy.
         */
        private void OnAttackHasKilled(Combat.Result result)
        {
            // @todo XP
            return;
        }

        /**
         * When the player performs a melee attack.
         */
        private void OnMeleeAttack()
        {
            gameLog.MeleeWeapons.LogAttack(fighter.MeleeWeapon.Data);
        }

        /**
         * When the player shoots its ranged weapon.
         */
        private void OnRangedShoot()
        {
            gameLog.RangedWeapons.LogShoot(fighter.RangedWeapon.Data);
        }

        /**
         * When an item is added to the inventory.
         */
        private void OnAddInventoryItem(Inventory.ItemData item)
        {
            return;
        }

        /**
         * When an item is removed from the inventory.
         */
        private void OnRemoveInventoryItem(Inventory.ItemData item)
        {
            return;
        }

        /**
         * When an item is equipped.
         */
        private void OnEquipItem(Equipment.EquipmentData equipmentItem)
        {
            // Add item attributes modifier to player
            fighter.AddTransientAttributes(equipmentItem.attributes);

            if (equipmentItem.type.isMelee)
            {
                fighter.MeleeWeapon.Data = equipmentItem.meleeData;
                fighter.MeleeWeapon.onAttack += OnMeleeAttack;
            }
            if (equipmentItem.type.isRanged)
            {
                fighter.RangedWeapon.Data = equipmentItem.rangedData;
                fighter.RangedWeapon.onShoot += OnRangedShoot;
            }

            // Remove from inventory (item can not be both in inventory and equipped).
            inventory.RemoveItem(equipmentItem.inventoryData);

            return;
        }

        /**
         * When an item is unequipped.
         */
        private void OnUnequipItem(Equipment.EquipmentData equipmentItem)
        {
            // Add item attributes modifier to player
            fighter.RemoveTransientAttributes(equipmentItem.attributes);

            if (equipmentItem.type.isMelee)
            {
                fighter.MeleeWeapon.Data = null;
                fighter.MeleeWeapon.onAttack -= OnMeleeAttack;
            }
            if (equipmentItem.type.isRanged)
            {
                fighter.RangedWeapon.Data = null;
                fighter.RangedWeapon.onShoot -= OnRangedShoot;
            }

            // Re-add the item to inventory.
            inventory.AddItem(equipmentItem.inventoryData);

        }

        [System.Serializable]
        public struct SaveData
        {

            public Combat.Fighter.SaveData fighter;
            public Inventory.InventoryManager.SaveData inventory;
            public Equipment.EquipmentManager.SaveData equipment;
            public GameLog.SaveData gameLog;

            public float[] position;
            public float[] rotation;
        };
        public object PrepareSaveData()
        {
            SaveData saveData = new();
            saveData.fighter = (Combat.Fighter.SaveData)fighter.PrepareSaveData();
            saveData.inventory = (Inventory.InventoryManager.SaveData)inventory.PrepareSaveData();
            saveData.equipment = (Equipment.EquipmentManager.SaveData)equipment.PrepareSaveData();
            saveData.gameLog = (GameLog.SaveData)gameLog.PrepareSaveData();

            saveData.position = new float[3];
            saveData.position[0] = transform.position.x;
            saveData.position[1] = transform.position.y;
            saveData.position[2] = transform.position.z;

            saveData.rotation = new float[4];
            saveData.rotation[0] = transform.rotation.w;
            saveData.rotation[1] = transform.rotation.x;
            saveData.rotation[2] = transform.rotation.y;
            saveData.rotation[3] = transform.rotation.z;

            return saveData;
        }
        public void RestoreSaveData(object save)
        {
            SaveData saveData = (SaveData)save;

            fighter.RestoreSaveData(saveData.fighter);
            inventory.RestoreSaveData(saveData.inventory);
            equipment.RestoreSaveData(saveData.equipment);
            gameLog.RestoreSaveData(saveData.gameLog);

            Vector3 pos = new(saveData.position[0], saveData.position[1], saveData.position[2]);
            Quaternion rot = new(saveData.rotation[0], saveData.rotation[1], saveData.rotation[2], saveData.rotation[3]);
            transform.SetPositionAndRotation(pos, rot);
        }
    }
}
