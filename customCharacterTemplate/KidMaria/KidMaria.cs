public class KidMaria : CustomCharacter
{
    public static CustomCharacterGeneralSettings KidMariaCharacterGeneralSettingsToggles = new()
    {
        CharacterName = "ALUCARD",
        CharacterMenuNameInput = "ALUCARD",
        CharacterUsesRPGSystem = true,
        CharacterDoesPrologue = true,
        CharacterHasCustomIntro = true,
        CharacterEncountersDeath = true
    };

    public static CustomCharacterInitialEquipment KidMariaCharacterInitialEquipmentItems = new()
    {
        RightHand = "Alucard sword",
        LeftHand = "Alucard shield",
        Head = "Dragon helm",
        Body = "Alucard mail",
        Cloak = "Twilight cloak",
        OtherSlotOne = "Necklace of J",
        OtherSlotTwo = "Empty"
    };

    public static CustomCharacterRPGElements KidMariaCharacterRPGSystemToggles = new()
    {
        /* REQUIRED */
        CharacterUsesEquipment = true,
        CharacterUsesMagic = true,
        CharacterUsesRelics = true,
        CharacterUsesFamiliars = true,

        /* OPTIONAL */
        CharacterInitialEquipment = CustomCharacterInitialEquipmentItems
    };

    public static CustomCharacterSprites KidMariaCharacterApparance = new()
    {
        CharacterSpritesAreDoubleSided = false,
        CharacterSpritesCollectionPath = "" // You will provide a path to your Custom Character's sprites here. Example: mods/KidMaria/Sprites
    };

    public KidMaria() : base(
        KidMariaCharacterGeneralSettingsToggles,
        KidMariaCharacterRPGSystemToggles,
        KidMariaCharacterApparance
    )
    {
    }
    
}