using Sotn;

public class CustomCharacter
{
    public struct CustomCharacterGeneralSettings {
        public required string CharacterName { get; set; } /* The name of the Character to Display on Pause Screen */
        public required string CharacterMenuNameInput { get; set; } /* The name to input on Main Menu to play as this character (SEE NOTE at Bottom of File) */
        public required bool CharacterUsesRPGSystem { get; set; } /* Does it use RPG System like Alucard, or not like Richter/Maria */
        public required bool CharacterDoesPrologue { get; set; } /* Does character do the prologue - Intro fight with Dracula */
        public required bool CharacterHasCustomIntro { get; set; } /* Does character have an intro like Alucard or do they just spawn in front of the door like Richter? */
        public required bool CharacterEncountersDeath { get; set; } /* Does character encounter Death in the Castle Entrance? */
    }

    public CustomCharacterGeneralSettings CustomCharacterGeneralSettingsToggles = new()
    {
        CharacterName = "ALUCARD",
        CharacterMenuNameInput = "ALUCARD",
        CharacterUsesRPGSystem = true,
        CharacterDoesPrologue = true,
        CharacterHasCustomIntro = true,
        CharacterEncountersDeath = true
    };

    public struct CustomCharacterInitialEquipment
    {
        public required string RightHand { get; set; } 
        public required string LeftHand { get; set; } 
        public required string Head { get; set; } 
        public required string Body { get; set; } 
        public required string Cloak { get; set; } 
        public required string OtherSlotOne { get; set; } 
        public required string OtherSlotTwo { get; set; } 
    }

    public static CustomCharacterInitialEquipment CustomCharacterInitialEquipmentItems = new()
    {
        RightHand = "Alucard sword",
        LeftHand = "Alucard shield",
        Head = "Dragon helm",
        Body = "Alucard mail",
        Cloak = "Twilight cloak",
        OtherSlotOne = "Necklace of J",
        OtherSlotTwo = "Empty"
    };

    public struct CustomCharacterRPGElements {
        /* REQUIRED */
        public required bool CharacterUsesEquipment { get; set; } /* Does character use Equip? - if so character should have initial equipment */
        public required bool CharacterUsesMagic { get; set; } /* Does character use Magic? - if so MP will show */
        public required bool CharacterUsesRelics { get; set; } /* Does character use Relics? - if so character can get upgrades via relics */
        public required bool CharacterUsesFamiliars { get; set; } /* Does character use Familiars? - if so they can level up familiars to fight alongside them */

        /* OPTIONAL */
        public CustomCharacterInitialEquipment CharacterInitialEquipment {get; set; } /* Characters Initial Equipment, if not provided they will start with nothing, whip, or birds only ! */
    }

    public CustomCharacterRPGElements CustomCharacterRPGSystemToggles = new()
    {
        /* REQUIRED */
        CharacterUsesEquipment = true,
        CharacterUsesMagic = true,
        CharacterUsesRelics = true,
        CharacterUsesFamiliars = true,

        /* OPTIONAL */
        CharacterInitialEquipment = CustomCharacterInitialEquipmentItems
    };

    public struct CustomCharacterSprites {
        public required bool CharacterSpritesAreDoubleSided { get; set; } /* Are your custom character's sprites double sided? If you don't know what this means, chances are the answer is no. */
        public required string CharacterSpritesCollectionPath { get; set; } /* Provide the path (starting from "SymphonyRecomp" directory) to your mods "Sprites" directory, if invalid path -> Your Character will look like Alucard, Richter, Or Maria */
    }

    public CustomCharacterSprites CustomCharacterApparance = new()
    {
        CharacterSpritesAreDoubleSided = false,
        CharacterSpritesCollectionPath = "" // You will provide a path to your Custom Character's sprites here. Example: mods/KidMaria/Sprites
    };

    public CustomCharacter(
        CustomCharacterGeneralSettings characterGeneralSettings,
        CustomCharacterRPGElements characterRPGElements,
        CustomCharacterSprites characterSprites
        
    )
    {
        characterGeneralSettings = CustomCharacterGeneralSettingsToggles;
        characterRPGElements =  CustomCharacterRPGSystemToggles;
        characterSprites = CustomCharacterApparance;
    }

        
    /* 
    NOTE: 
    
    If character shares the same name as another character, RecompOne will ask "Which One?"

    If you wish the character to be used as "Richter" "Alucard" or "Maria" in a "Character Select Mod" then 
    set the characters name as Richter, Alucard, or Maria. If not, use another name besides those 3 and it
    will show up as a "Custom" entry. 
    
    To have a custom Character Select picture, provide a "CharacterSelect.png" for your character in
    the mod folder.
    */
}