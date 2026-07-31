using RecompOne.Runtime.Context;
using RecompOne.Runtime.Dispatch;
using RecompOne.Runtime.Memory;

namespace Sotn;

public enum FadeMode
{
    None = 0,
    ToBlack = 1,
    FromBlack = 2,
    BlueTint = 3,
    ToBlackFast = 4,
    ToBlackSlow = 5,
    ShowMap = 6,
    HideMap = 7,
}

public static class GameApi
{
    public const uint FreePrimitivesAddr = 0x8003C7B4u;
    public const uint AllocPrimitivesAddr = 0x8003C7B8u;
    public const uint CheckCollisionAddr = 0x8003C7BCu;
    public const uint UpdateAnimAddr = 0x8003C7C4u;
    public const uint SetSpeedXAddr = 0x8003C7C8u;
    public const uint GetFreeEntityAddr = 0x8003C7CCu;
    public const uint GetEquipPropertiesAddr = 0x8003C7D0u;
    public const uint LoadGfxAsyncAddr = 0x8003C7D8u;
    public const uint PlaySfxAddr = 0x8003C7DCu;
    public const uint SetFadeModeAddr = 0x8003C7ECu;
    public const uint CreateEntFactoryAddr = 0x8003C7F4u;
    public const uint EnemyDefsAddr = 0x8003C808u;
    public const uint UpdateUnarmedAnimAddr = 0x8003C814u;
    public const uint PlayAnimationAddr = 0x8003C818u;
    public const uint DealDamageAddr = 0x8003C828u;
    public const uint LoadEquipIconAddr = 0x8003C82Cu;
    public const uint EquipDefsAddr = 0x8003C830u;
    public const uint AccessoryDefsAddr = 0x8003C834u;
    public const uint AddHeartsAddr = 0x8003C838u;
    public const uint LoadMonsterLibrarianPreviewAddr = 0x8003C83Cu;
    public const uint TimeAttackControllerAddr = 0x8003C840u;
    public const uint ForceAfterImageOnAddr = 0x8003C844u;
    public const uint AddToInventoryAddr = 0x8003C84Cu;
    public const uint RelicDefsAddr = 0x8003C850u;
    public const uint InitStatsAndGearAddr = 0x8003C854u;
    public const uint PlaySfxVolPanAddr = 0x8003C858u;
    public const uint SetVolumeCommandAddr = 0x8003C85Cu;
    public const uint CheckEquipmentItemCountAddr = 0x8003C864u;
    public const uint GetPlayerSensorAddr = 0x8003C868u;
    public const uint RevealSecretPassageAddr = 0x8003C86Cu;
    public const uint GetServantStatsAddr = 0x8003C874u;
    public const uint CdSoundCommandQueueEmptyAddr = 0x8003C880u;
    public const uint GetStatBuffTimerAddr = 0x8003C88Cu;
    public const uint CalcPlayerDamageAddr = 0x8003C894u;
    public const uint LearnSpellAddr = 0x8003C898u;

    static IMemory M => RecompOne.Runtime.Runtime.Mem!;

    public static uint Call(CpuContext c, IMemory m, uint funcAddr, uint a0 = 0, uint a1 = 0, uint a2 = 0, uint a3 = 0)
    {
        var snap = c.Snapshot();
        c.A0 = a0;
        c.A1 = a1;
        c.A2 = a2;
        c.A3 = a3;
        Dispatcher.Call(c, m, funcAddr);
        uint ret = c.V0;
        c.Restore(snap);
        return ret;
    }

    public static uint Call(uint funcAddr, uint a0 = 0, uint a1 = 0, uint a2 = 0, uint a3 = 0)
    {
        var c = RecompOne.Runtime.Runtime.Cpu;
        var m = RecompOne.Runtime.Runtime.Mem;
        if (c == null || m == null) return 0;
        return Call(c, m, funcAddr, a0, a1, a2, a3);
    }

    //add game calls here
    public static uint CallApi(uint apiSlot, uint a0 = 0, uint a1 = 0, uint a2 = 0, uint a3 = 0)=> Call(M.ReadU32(apiSlot), a0, a1, a2, a3);
    public static uint CallApi(CpuContext c, IMemory m, uint apiSlot, uint a0 = 0, uint a1 = 0, uint a2 = 0, uint a3 = 0) => Call(c, m, m.ReadU32(apiSlot), a0, a1, a2, a3);

    public static void FreePrimitives(int primIndex) => CallApi(FreePrimitivesAddr, (uint)primIndex);
    public static int AllocPrimitives(int type, int count) => (int)(short)CallApi(AllocPrimitivesAddr, (uint)type, (uint)count);
    public static Entity GetFreeEntity(int start, int end) => new(CallApi(GetFreeEntityAddr, (uint)start, (uint)end));
    public static Entity CreateFactory(Entity self, uint flags, int arg2) => new(CallApi(CreateEntFactoryAddr, self.Addr, flags, (uint)arg2));
    public static void PlaySfx(int sfxId) => CallApi(PlaySfxAddr, (uint)sfxId);
    public static int PlaySfxVolPan(int sfxId, int volume, int pan) => (int)CallApi(PlaySfxVolPanAddr, (uint)sfxId, (uint)volume, (uint)pan);
    public static void AddHearts(int amount) => CallApi(AddHeartsAddr, (uint)amount);
    public static void LearnSpell(Spell spell) => CallApi(LearnSpellAddr, (uint)spell);
    public static void AddToInventory(int id, int kind) => CallApi(AddToInventoryAddr, (uint)id, (uint)kind);
    public static void AddToInventory(HandItem item) => AddToInventory((int)item, (int)EquipKind.Hand);
    public static void AddToInventory(BodyItem item) => AddToInventory((int)item, (int)Inventory.KindOf(item));
    public static int DealDamage(Entity enemy, Entity attacker) => (int)(ushort)CallApi(DealDamageAddr, enemy.Addr, attacker.Addr);
    public static void SetFadeMode(FadeMode mode) => CallApi(SetFadeModeAddr, (uint)mode);
    public static void SetSpeedX(int value) => CallApi(SetSpeedXAddr, (uint)value);
    public static void LoadGfxAsync(int gfxId) => CallApi(LoadGfxAsyncAddr, (uint)gfxId);
    public static void InitStatsAndGear(bool debugMode) => CallApi(InitStatsAndGearAddr, debugMode ? 1u : 0u);
    public static void ForceAfterImageOn() => CallApi(ForceAfterImageOnAddr);
    public static void LoadEquipIcon(int equipIcon, int palette, int index) => CallApi(LoadEquipIconAddr, (uint)equipIcon, (uint)palette, (uint)index);
    public static bool LoadMonsterLibrarianPreview(int monsterId) => CallApi(LoadMonsterLibrarianPreviewAddr, (uint)monsterId) != 0;
    public static int TimeAttackController(TimeAttackEvent eventId, int action) => (int)CallApi(TimeAttackControllerAddr, (uint)eventId, (uint)action);
    public static int CheckEquipmentItemCount(int itemId, int equipType) => (int)CallApi(CheckEquipmentItemCountAddr, (uint)itemId, (uint)equipType);
    public static void RevealSecretPassage(int arg0) => CallApi(RevealSecretPassageAddr, (uint)arg0);
    public static int GetStatBuffTimer(int arg0) => (int)CallApi(GetStatBuffTimerAddr, (uint)arg0);
    public static bool CdSoundCommandQueueEmpty() => CallApi(CdSoundCommandQueueEmptyAddr) != 0;

    public static uint EnemyDefs => M.ReadU32(EnemyDefsAddr);
    public static uint EquipDefs => M.ReadU32(EquipDefsAddr);
    public static uint AccessoryDefs => M.ReadU32(AccessoryDefsAddr);
    public static uint RelicDefs => M.ReadU32(RelicDefsAddr);
}
