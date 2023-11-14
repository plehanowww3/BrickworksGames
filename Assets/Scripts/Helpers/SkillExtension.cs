using Enums;
using UnityEngine;

namespace Helpers
{
    /// <summary>
    /// Расширение для работы со способностями
    /// </summary>
    public static class SkillExtension
    {
        /// <summary>
        /// Получить имя способности
        /// </summary>
        /// <param name="_skillEnum">Тип способности</param>
        /// <returns>Название способности</returns>
        public static string GetSkillName(SkillsEnum _skillEnum)
        {
            switch (_skillEnum)
            {
                case SkillsEnum.BASE_SKILL:
                    return "BaseSkill";
                case SkillsEnum.INCREASE_HEALTH_1:
                    return "Increase Health 1";
                case SkillsEnum.INCREASE_HEALTH_2:
                    return "Increase Health 2";
                case SkillsEnum.INCREASE_HEALTH_3:
                    return "Increase Health 3";
                case SkillsEnum.ORATORY:
                    return "Oratory";
                case SkillsEnum.SWORD_DAMAGE:
                    return "Sword Damage";
                case SkillsEnum.DAGGER_DAMAGE:
                    return "Dagger Damage";
                case SkillsEnum.BETTER_PRICES:
                    return "Better Prices";
                case SkillsEnum.CRITICAL_DAMAGE:
                    return "Critical Damage";
                default:
                    Debug.LogWarning("Cant find skill with that enum type");
                    return "";
            }
        }
    }
}