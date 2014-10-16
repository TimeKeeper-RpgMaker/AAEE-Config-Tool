using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AAEE.Configurations;
using AAEE.Data;
using AAEE.DataPacks;
using AAEE.Utility;

namespace AAEE.Data
{
    public static class Generator
    {
        #region Fields

        private static Configuration cfg;
        private static GameData gameData;
        private static int depth;
        private static int position;
        private static int index;
        private static int column;

        #endregion

        #region Generator

        public static string GenerateScript(Configuration newCfg, GameData newGameData)
        {
            cfg = newCfg;
            gameData = newGameData;
            depth = 0;
            return genScript();
        }

        #endregion

        #region Utility

        private static string decimalToString(decimal stringValue)
        {
            return stringValue.ToString("0.0#########");
        }

        private static string genSigns(string sign)
        {
            int times = (78 - depth * 2) / sign.Length;
            string script = "#";
            for (int i = 0; i < times; i++)
            {
                script += sign;
            }
            return script;
        }

        private static string genIndent()
        {
            string script = "";
            for (int i = 0; i < depth; i++)
            {
                script += "  ";
            }
            return script;
        }

        private static string genNewLine(string input)
        {
            return (System.Environment.NewLine + genIndent() + input);
        }

        private static string genNewLine()
        {
            return genNewLine("");
        }

        private static string genName(int id, string name)
        {

            return (id.ToString() + "_" + name.Replace(" ", "_"));
        }

        #endregion

        #region Generate Simple

        #region Module

        private static string genModuleHeader(string name, string specs)
        {
            string script = genNewLine(genSigns(":="));
            script += genNewLine("# AAEE::" + name);
            script += genNewLine(genSigns("-"));
            script += genNewLine("#  This module provides " + specs + " configurations.");
            script += genNewLine(genSigns(":="));
            script += genNewLine("module " + name);
            depth++;
            return script;
        }

        private static string genModuleEnd()
        {
            depth--;
            return genNewLine() + "end";
        }

        #endregion

        #region Hash

        private static string genHashNewLine(string input)
        {
            string script = genNewLine(input);
            if (position > 0 && column > 1)
            {
                if (position % column == 0)
                {
                    script = "," + genNewLine(input);
                }
                else
                {
                    script = ", " + input;
                }
            }
            else if (index > 0)
            {
                script = "," + genNewLine(input);
            }
            return script;
        }

        #region Single Value

        private static string genHashSingle(string hashName, int hashValue)
        {
            return genHashNewLine(":" + hashName + " => " + hashValue.ToString());
        }

        private static string genHashSingle(string hashName, decimal hashValue)
        {
            return genHashNewLine(":" + hashName + " => " + decimalToString(hashValue));
        }

        private static string genHashSingle(string hashName, string hashValue)
        {
            return genHashNewLine(":" + hashName + " => " + hashValue);
        }

        private static string genHashSingle(string hashName, bool hashValue)
        {
            return genHashNewLine(":" + hashName + " => " + (hashValue.ToString()).ToLower());
        }

        #endregion

        #region Double Value

        private static string genHashDouble(string hashName, int hashValue1, int hashValue2)
        {
            return genHashNewLine(":" + hashName + " => [" + hashValue1.ToString() + ", " + hashValue2.ToString() + "]");
        }

        private static string genHashDouble(string hashName, decimal hashValue1, int hashValue2)
        {
            return genHashNewLine(":" + hashName + " => [" + decimalToString(hashValue1) + ", " + hashValue2.ToString() + "]");
        }

        private static string genHashDouble(string hashName, int hashValue1, decimal hashValue2)
        {
            return genHashNewLine(":" + hashName + " => [" + hashValue1.ToString() + ", " + decimalToString(hashValue2) + "]");
        }

        private static string genHashDouble(string hashName, decimal hashValue1, decimal hashValue2)
        {
            return genHashNewLine(":" + hashName + " => [" + decimalToString(hashValue1) + ", " + decimalToString(hashValue2) + "]");
        }

        private static string genHashDouble(string hashName, string hashValue1, string hashValue2)
        {
            return genHashNewLine(":" + hashName + " => [" + hashValue1 + ", " + hashValue2 + "]");
        }

        #endregion

        #endregion

        #region Variable

        private static string genVarSingle(string varName, int varValue)
        {
            return genNewLine(varName + " = " + varValue.ToString());
        }

        private static string genVarSingle(string varName, bool varValue)
        {
            return genNewLine(varName + " = " + (varValue.ToString()).ToLower());
        }

        private static string genVarArray(string varName, int[] varArray)
        {
            int tempPos = 0;
            string script = varName + " = [";
            foreach (int i in varArray)
            {
                if (tempPos > 0)
                {
                    script += ", ";
                }
                script += i.ToString();
                tempPos++;
            }
            script += "]";
            return genNewLine(script);
        }

        #endregion

        #endregion

        #region Generate Complex

        #region Actor

        private static string genDataPackActor(DataPackActor actor)
        {
            bool scriptCheck = false;
            index = 0;
            position = 0;

            string script = "";

            #region Equipment Name List

            if (actor.EquipType.Count > 0)
            {
                scriptCheck = false;
                if (actor.EquipType.Count > 5)
                {
                    scriptCheck = true;
                }
                else
                {
                    foreach (EquipType equip in actor.EquipType)
                    {
                        if (equip.Name != cfg.Default.EquipType[equip.ID].Name)
                        {
                            scriptCheck = true;
                        }
                    }
                }
                if (scriptCheck)
                {
                    position = 0;
                    column = 4;
                    script += genNewLine(":EquipName => { ");
                    depth++;
                    foreach (EquipType equip in actor.EquipType)
                    {
                        if (equip.ID > 4)
                        {
                            if (equip.Name != "")
                            {
                                script += genHashNewLine(equip.ID.ToString() + " => " + "'" + equip.Name + "'");
                                position++;
                            }
                            else
                            {
                                script += genHashNewLine(equip.ID.ToString() + " => " + "'" + actor.EquipType[4].Name + "'");
                                position++;
                            }
                        }
                        else
                        {
                            if (equip.Name != AAEEData.EquipTypeName[equip.ID])
                            {
                                script += genHashNewLine(equip.ID.ToString() + " => " + "'" + equip.Name + "'");
                                position++;
                            }
                        }
                    }
                    depth--;
                    script += genNewLine("}");
                    index++;
                }
            }

            #endregion

            position = 0;
            column = 3;

            #region Equipment

            // Write the equip id list
            if (actor.EquipList.Count > 0)
            {
                script += genHashNewLine(":EquipList => {");
                foreach (EquipType equip in actor.EquipList)
                {
                    if (position > 0)
                    {
                        script += ", ";
                    }
                    if (equip.Name != cfg.Default.EquipType[equip.ID].Name)
                    {
                        script += equip.ID.ToString() + " => '" + equip.Name + "'";
                    }
                    else
                    {
                        script += equip.ID.ToString() + " => nil";
                    }
                    position++;
                }
                script += "}";
                index++;
            }

            // Dual Hold
            if (actor.DualHold)
            {
                script += genHashSingle("DualHold", (actor.DualHold.ToString()).ToLower());
                position++;
                index++;

                // Dual hold name
                if (actor.CustomDualHoldName && (actor.DualHoldNameWeapon != cfg.Default.DualHoldNameWeapon || actor.DualHoldNameShield != cfg.Default.DualHoldNameShield))
                {
                    script += genHashDouble("DualHoldName", actor.DualHoldNameWeapon, actor.DualHoldNameShield);
                    position++;
                    index++;
                }

                // Dual hold multiplier
                if (actor.CustomDualHoldMul && (actor.DualHoldMulWeapon != cfg.Default.DualHoldMulWeapon || actor.DualHoldMulShield != cfg.Default.DualHoldMulShield))
                {
                    script += genHashDouble("DualHold_Mul", actor.DualHoldMulWeapon, actor.DualHoldMulShield);
                    position++;
                    index++;
                }

                // Shield bypass
                if (actor.ShieldBypass)
                {
                    script += genHashSingle("OffHandBypass", actor.ShieldBypass);
                    position++;
                    index++;

                    // Shield bypass multiplier
                    if (actor.ShieldBypassMul != cfg.Default.ShieldBypassMul)
                    {
                        script += genHashSingle("OffHandAtkMul", actor.ShieldBypassMul);
                        position++;
                        index++;
                    }
                }
            }

            // Weapon bypass
            if (actor.WeaponBypass)
            {
                script += genHashSingle("MainHandBypass", actor.ShieldBypass);
                position++;
                index++;

                // Weapon bypass multiplier
                if (actor.WeaponBypassMul != cfg.Default.WeaponBypassMul)
                {
                    script += genHashSingle("MainHandAtkMul", actor.WeaponBypassMul);
                    position++;
                    index++;
                }
            }

            // Reduce hand
            if (actor.CustomReduceHand)
            {
                if (actor.ReduceHand != cfg.Default.ReduceHand)
                {
                    script += genHashSingle("HandNeedReduce", actor.ReduceHand);
                    position++;
                    index++;
                }

                // Reduce hand multiplier
                if (actor.ReduceHandMul != cfg.Default.ReduceHandMul)
                {
                    script += genHashSingle("HandReduceMul", actor.ReduceHandMul);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Parameter

            // Maximum HP
            if (actor.CustomMaxHP && (actor.MaxHPInitial != cfg.Default.MaxHPInitial || actor.MaxHPFinal != cfg.Default.MaxHPFinal))
            {
                script += genHashDouble("MaxHP", actor.MaxHPInitial, actor.MaxHPFinal);
                position++;
                index++;
            }

            // Maximum SP
            if (actor.CustomMaxSP && (actor.MaxSPInitial != cfg.Default.MaxSPInitial || actor.MaxSPFinal != cfg.Default.MaxSPFinal))
            {
                script += genHashDouble("MaxSP", actor.MaxSPInitial, actor.MaxSPFinal);
                position++;
                index++;
            }

            // Strengh
            if (actor.CustomStr && (actor.StrInitial != cfg.Default.StrInitial || actor.StrFinal != cfg.Default.StrFinal))
            {
                script += genHashDouble("Str", actor.StrInitial, actor.StrFinal);
                position++;
                index++;
            }

            // Dexterity
            if (actor.CustomDex && (actor.DexInitial != cfg.Default.DexInitial || actor.DexFinal != cfg.Default.DexFinal))
            {
                script += genHashDouble("Dex", actor.DexInitial, actor.DexFinal);
                position++;
                index++;
            }

            // Agility
            if (actor.CustomAgi && (actor.AgiInitial != cfg.Default.AgiInitial || actor.AgiFinal != cfg.Default.AgiFinal))
            {
                script += genHashDouble("Agi", actor.AgiInitial, actor.AgiFinal);
                position++;
                index++;
            }

            // Intelligence
            if (actor.CustomInt && (actor.IntInitial != cfg.Default.IntInitial || actor.IntFinal != cfg.Default.IntFinal))
            {
                script += genHashDouble("Int", actor.IntInitial, actor.IntFinal);
                position++;
                index++;
            }

            #endregion

            #region Parameter Rate

            // Strengh attack rate
            if (actor.CustomStrRate && actor.StrRate != cfg.Default.StrRate)
            {
                script += genHashSingle("StrRate", actor.StrRate);
                position++;
                index++;
            }

            // Dexterity attack rate
            if (actor.CustomDexRate && actor.DexRate != cfg.Default.DexRate)
            {
                script += genHashSingle("DexRate", actor.DexRate);
                position++;
                index++;
            }

            // Agility attack rate
            if (actor.CustomAgiRate && actor.AgiRate != cfg.Default.AgiRate)
            {
                script += genHashSingle("AgiRate", actor.AgiRate);
                position++;
                index++;
            }

            // Intelligence attack rate
            if (actor.CustomIntRate && actor.IntRate != cfg.Default.IntRate)
            {
                script += genHashSingle("IntRate", actor.IntRate);
                position++;
                index++;
            }

            // Physical defense attack rate
            if (actor.CustomPDefRate && actor.PDefRate != cfg.Default.PDefRate)
            {
                script += genHashSingle("PDefRate", actor.PDefRate);
                position++;
                index++;
            }

            // Magical defense attack rate
            if (actor.CustomMDefRate && actor.MDefRate != cfg.Default.MDefRate)
            {
                script += genHashSingle("MDefRate", actor.MDefRate);
                position++;
                index++;
            }

            // Guard rate
            if (actor.CustomGuardRate && actor.GuardRate != cfg.Default.GuardRate)
            {
                script += genHashSingle("GuardRate", actor.GuardRate);
                position++;
                index++;
            }

            // Evasion rate
            if (actor.CustomEvaRate && actor.EvaRate != cfg.Default.EvaRate)
            {
                script += genHashSingle("EvaRate", actor.EvaRate * 100);
                position++;
                index++;
            }

            #endregion

            #region Defense against Attack Critical

            // Defense against attack critical rate
            if (actor.CustomDefCritRate && actor.DefCritRate != cfg.Default.DefCritRate)
            {
                script += genHashSingle("Def_CritRate", actor.DefCritRate);
                position++;
                index++;
            }

            // Defense against attack critical damage
            if (actor.CustomDefCritDamage && actor.DefCritDamage != cfg.Default.DefCritDamage)
            {
                script += genHashSingle("Def_CritDamage", actor.DefCritDamage);
                position++;
                index++;
            }

            // Defense against attack special critical rate
            if (actor.CustomDefSpCritRate && actor.DefSpCritRate != cfg.Default.DefSpCritRate)
            {
                script += genHashSingle("Def_SpCritRate", actor.DefSpCritRate);
                position++;
                index++;
            }

            // Defense against attack special critical damage
            if (actor.CustomDefSpCritDamage && actor.DefSpCritDamage != cfg.Default.DefSpCritDamage)
            {
                script += genHashSingle("Def_SpCritDamage", actor.DefSpCritDamage);
                position++;
                index++;
            }

            #endregion

            #region Defense against Skill Critical

            // Defense against skill critical rate
            if (actor.CustomDefSkillCritRate && actor.DefSkillCritRate != cfg.Default.DefSkillCritRate)
            {
                script += genHashSingle("Def_Skill_CritRate", actor.DefSkillCritRate);
                position++;
                index++;
            }

            // Defense against skill critical damage
            if (actor.CustomDefSkillCritDamage && actor.DefSkillCritDamage != cfg.Default.DefSkillCritDamage)
            {
                script += genHashSingle("Def_Skill_CritDamage", actor.DefSkillCritDamage);
                position++;
                index++;
            }

            // Defense against skill special critical rate
            if (actor.CustomDefSkillSpCritRate && actor.DefSkillSpCritRate != cfg.Default.DefSkillSpCritRate)
            {
                script += genHashSingle("Def_Skill_SpCritRate", actor.DefSkillSpCritRate);
                position++;
                index++;
            }

            // Defense against skill special critical damage
            if (actor.CustomDefSkillSpCritDamage && actor.DefSkillSpCritDamage != cfg.Default.DefSkillSpCritDamage)
            {
                script += genHashSingle("Def_Skill_SpCritDamage", actor.DefSkillSpCritDamage);
                position++;
                index++;
            }

            #endregion

            #region Unarmed Attack

            // Unarmed attack
            if (actor.CustomAtk && (actor.AtkInitial != cfg.Default.AtkInitial || actor.AtkFinal != cfg.Default.AtkFinal))
            {
                script += genHashDouble("Atk", actor.AtkInitial, actor.AtkFinal);
                position++;
                index++;
            }

            // Unarmed hit rate
            if (actor.CustomHit && (actor.HitInitial != cfg.Default.HitInitial || actor.HitFinal != cfg.Default.HitFinal))
            {
                script += genHashDouble("HitRate", actor.HitInitial * 100, actor.HitFinal * 100);
                position++;
                index++;
            }

            // Unarmed attack animation
            if (actor.CustomAnim && (actor.AnimCaster != cfg.Default.AnimCaster || actor.AnimTarget != cfg.Default.AnimTarget))
            {
                script += genHashDouble("Anim", actor.AnimCaster, actor.AnimTarget);
                position++;
                index++;
            }

            // Unarmed strengh attack force
            if (actor.CustomParamForce)
            {
                if (actor.StrForce != cfg.Default.StrForce)
                {
                    script += genHashSingle("Str_Force", actor.StrForce);
                    position++;
                    index++;
                }

                // Unarmed dexterity attack force
                if (actor.DexForce != cfg.Default.DexForce)
                {
                    script += genHashSingle("Dex_Force", actor.DexForce);
                    position++;
                    index++;
                }

                // Unarmed agility attack force
                if (actor.AgiForce != cfg.Default.AgiForce)
                {
                    script += genHashSingle("Agi_Force", actor.AgiForce);
                    position++;
                    index++;
                }

                // Unarmed intelligence attack force
                if (actor.IntForce != cfg.Default.IntForce)
                {
                    script += genHashSingle("Int_Force", actor.IntForce);
                    position++;
                    index++;
                }
            }

            // Unarmed physical defense attack force
            if (actor.CustomDefenseForce)
            {
                if (actor.PDefForce != cfg.Default.PDefForce)
                {
                    script += genHashSingle("PDef_Force", actor.PDefForce);
                    position++;
                    index++;
                }

                // Unarmed magical defense attack force
                if (actor.MDefForce != cfg.Default.MDefForce)
                {
                    script += genHashSingle("MDef_Force", actor.MDefForce);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Unarmed Critical Rate

            // Unarmed critical rate
            if (actor.CustomCritRate && actor.CritRate != cfg.Default.CritRate)
            {
                script += genHashSingle("CritRate", actor.CritRate * 100);
                position++;
                index++;
            }

            // Unarmed critical damage
            if (actor.CustomCritDamage && actor.CritDamage != cfg.Default.CritDamage)
            {
                script += genHashSingle("CritDamage", actor.CritDamage);
                position++;
                index++;
            }

            // Unarmed critical guard rate reduction
            if (actor.CustomCritDefGuard && actor.CritDefGuard != cfg.Default.CritDefGuard)
            {
                script += genHashSingle("Crit_DefGuard", actor.CritDefGuard);
                position++;
                index++;
            }

            // Unarmed critical evasion rate reduction
            if (actor.CustomCritDefEva && actor.CritDefEva != cfg.Default.CritDefEva)
            {
                script += genHashSingle("Crit_DefEva", actor.CritDefEva);
                position++;
                index++;
            }

            #endregion

            #region Unarmed Special Critical Rate

            // Unarmed special critical rate
            if (actor.CustomSpCritRate && actor.SpCritRate != cfg.Default.SpCritRate)
            {
                script += genHashSingle("SpCritRate", actor.SpCritRate * 100);
                position++;
                index++;
            }

            // Unarmed special critical damage
            if (actor.CustomSpCritDamage && actor.SpCritDamage != cfg.Default.SpCritDamage)
            {
                script += genHashSingle("SpCritDamage", actor.SpCritDamage);
                position++;
                index++;
            }

            // Unarmed special critical guard rate reduction
            if (actor.CustomSpCritDefGuard && actor.SpCritDefGuard != cfg.Default.SpCritDefGuard)
            {
                script += genHashSingle("SpCrit_DefGuard", actor.SpCritDefGuard);
                position++;
                index++;
            }

            // Unarmed special critical evasion rate reduction
            if (actor.CustomSpCritDefEva && actor.SpCritDefEva != cfg.Default.SpCritDefEva)
            {
                script += genHashSingle("SpCrit_DefEva", actor.SpCritDefEva);
                position++;
                index++;
            }

            #endregion

            #region Unarmoured Defense

            // Unarmoured Physical Defense
            if (actor.CustomPDef && (actor.PDefInitial != cfg.Default.PDefInitial || actor.PDefFinal != cfg.Default.PDefFinal))
            {
                script += genHashDouble("PDef", actor.PDefInitial, actor.PDefFinal);
                position++;
                index++;
            }

            // Unarmoured Magical Defense
            if (actor.CustomMDef && (actor.MDefInitial != cfg.Default.MDefInitial || actor.MDefFinal != cfg.Default.MDefFinal))
            {
                script += genHashDouble("MDef", actor.MDefInitial, actor.MDefFinal);
                position++;
                index++;
            }

            #endregion

            return script;
        }

        #endregion

        #region Class

        private static string genDataPackClass(DataPackClass classes)
        {
            bool scriptCheck = false;
            index = 0;
            position = 0;

            string script = "";

            #region Equipment Name List

            if (classes.EquipType.Count > 0)
            {
                scriptCheck = false;
                if (classes.EquipType.Count > 5)
                {
                    scriptCheck = true;
                }
                else
                {
                    foreach (EquipType equip in classes.EquipType)
                    {
                        if (equip.Name != cfg.Default.EquipType[equip.ID].Name)
                        {
                            scriptCheck = true;
                        }
                    }
                }
                if (scriptCheck)
                {
                    position = 0;
                    column = 4;
                    script += genNewLine(":EquipName => { ");
                    depth++;
                    foreach (EquipType equip in classes.EquipType)
                    {
                        if (equip.ID > 4)
                        {
                            if (equip.Name != "")
                            {
                                script += genHashNewLine(equip.ID.ToString() + " => " + "'" + equip.Name + "'");
                                position++;
                            }
                            else
                            {
                                script += genHashNewLine(equip.ID.ToString() + " => " + "'" + classes.EquipType[4].Name + "'");
                                position++;
                            }
                        }
                        else
                        {
                            if (equip.Name != AAEEData.EquipTypeName[equip.ID])
                            {
                                script += genHashNewLine(equip.ID.ToString() + " => " + "'" + equip.Name + "'");
                                position++;
                            }
                        }
                    }
                    depth--;
                    script += genNewLine("}");
                    index++;
                }
            }

            #endregion

            position = 0;
            column = 3;

            #region Equipment

            // Write the equip id list
            if (classes.EquipList.Count > 0)
            {
                script += genHashNewLine(":EquipList => {");
                foreach (EquipType equip in classes.EquipList)
                {
                    if (position > 0)
                    {
                        script += ", ";
                    }
                    if (equip.Name != cfg.Default.EquipType[equip.ID].Name)
                    {
                        script += equip.ID.ToString() + " => '" + equip.Name + "'";
                    }
                    else
                    {
                        script += equip.ID.ToString() + " => nil";
                    }
                    position++;
                }
                script += "}";
                index++;
            }

            // Dual Hold
            if (classes.DualHold)
            {
                script += genHashSingle("DualHold", (classes.DualHold.ToString()).ToLower());
                position++;
                index++;

                // Dual hold name
                if (classes.CustomDualHoldName && (classes.DualHoldNameWeapon != cfg.Default.DualHoldNameWeapon || classes.DualHoldNameShield != cfg.Default.DualHoldNameShield))
                {
                    script += genHashDouble("DualHoldName", classes.DualHoldNameWeapon, classes.DualHoldNameShield);
                    position++;
                    index++;
                }

                // Dual hold multiplier
                if (classes.CustomDualHoldMul && (classes.DualHoldMulWeapon != cfg.Default.DualHoldMulWeapon || classes.DualHoldMulShield != cfg.Default.DualHoldMulShield))
                {
                    script += genHashDouble("DualHold_Mul", classes.DualHoldMulWeapon, classes.DualHoldMulShield);
                    position++;
                    index++;
                }

                // Shield bypass
                if (classes.ShieldBypass)
                {
                    script += genHashSingle("OffHandBypass", classes.ShieldBypass);
                    position++;
                    index++;

                    // Shield bypass multiplier
                    if (classes.ShieldBypassMul != cfg.Default.ShieldBypassMul)
                    {
                        script += genHashSingle("OffHandAtkMul", classes.ShieldBypassMul);
                        position++;
                        index++;
                    }
                }
            }

            // Weapon bypass
            if (classes.WeaponBypass)
            {
                script += genHashSingle("MainHandBypass", classes.ShieldBypass);
                position++;
                index++;

                // Weapon bypass multiplier
                if (classes.WeaponBypassMul != cfg.Default.WeaponBypassMul)
                {
                    script += genHashSingle("MainHandAtkMul", classes.WeaponBypassMul);
                    position++;
                    index++;
                }
            }

            // Reduce hand
            if (classes.CustomReduceHand)
            {
                if (classes.ReduceHand != cfg.Default.ReduceHand)
                {
                    script += genHashSingle("HandNeedReduce", classes.ReduceHand);
                    position++;
                    index++;
                }

                // Reduce hand multiplier
                if (classes.ReduceHandMul != cfg.Default.ReduceHandMul)
                {
                    script += genHashSingle("HandReduceMul", classes.ReduceHandMul);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Parameter

            // Maximum HP
            if (classes.CheckMaxHP())
            {
                if (classes.MaxHPMul != 0)
                {
                    script += genHashSingle("MaxHP_Mul", classes.MaxHPMul);
                    position++;
                    index++;
                }
                if (classes.MaxHPAdd != 0)
                {
                    script += genHashSingle("MaxHP_Add", classes.MaxHPAdd);
                    position++;
                    index++;
                }
            }

            // Maximum HP
            if (classes.CheckMaxSP())
            {
                if (classes.MaxSPMul != 0)
                {
                    script += genHashSingle("MaxSP_Mul", classes.MaxSPMul);
                    position++;
                    index++;
                }
                if (classes.MaxSPAdd != 0)
                {
                    script += genHashSingle("MaxSP_Add", classes.MaxSPAdd);
                    position++;
                    index++;
                }
            }

            // Strengh
            if (classes.CheckStr())
            {
                if (classes.StrMul != 0)
                {
                    script += genHashSingle("Str_Mul", classes.StrMul);
                    position++;
                    index++;
                }
                if (classes.StrAdd != 0)
                {
                    script += genHashSingle("Str_Add", classes.StrAdd);
                    position++;
                    index++;
                }
            }

            // Dexterity
            if (classes.CheckDex())
            {
                if (classes.DexMul != 0)
                {
                    script += genHashSingle("Dex_Mul", classes.DexMul);
                    position++;
                    index++;
                }
                if (classes.DexAdd != 0)
                {
                    script += genHashSingle("Dex_Add", classes.DexAdd);
                    position++;
                    index++;
                }
            }

            // Agility
            if (classes.CheckAgi())
            {
                if (classes.AgiMul != 0)
                {
                    script += genHashSingle("Agi_Mul", classes.AgiMul);
                    position++;
                    index++;
                }
                if (classes.AgiAdd != 0)
                {
                    script += genHashSingle("Agi_Add", classes.AgiAdd);
                    position++;
                    index++;
                }
            }

            // Intelligence
            if (classes.CheckInt())
            {
                if (classes.IntMul != 0)
                {
                    script += genHashSingle("Int_Mul", classes.IntMul);
                    position++;
                    index++;
                }
                if (classes.IntAdd != 0)
                {
                    script += genHashSingle("Int_Add", classes.IntAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Parameter Rate

            // Intelligence
            if (classes.CheckStrRate())
            {
                if (classes.StrRateMul != 0)
                {
                    script += genHashSingle("StrRate_Mul", classes.StrRateMul);
                    position++;
                    index++;
                }
                if (classes.StrRateAdd != 0)
                {
                    script += genHashSingle("StrRate_Add", classes.StrRateAdd);
                    position++;
                    index++;
                }
            }

            // Dexterity attack rate
            if (classes.CheckDexRate())
            {
                if (classes.DexRateMul != 0)
                {
                    script += genHashSingle("DexRate_Mul", classes.DexRateMul);
                    position++;
                    index++;
                }
                if (classes.DexRateAdd != 0)
                {
                    script += genHashSingle("DexRate_Add", classes.DexRateAdd);
                    position++;
                    index++;
                }
            }

            // Agility attack rate
            if (classes.CheckAgiRate())
            {
                if (classes.AgiRateMul != 0)
                {
                    script += genHashSingle("AgiRate_Mul", classes.AgiRateMul);
                    position++;
                    index++;
                }
                if (classes.AgiRateAdd != 0)
                {
                    script += genHashSingle("AgiRate_Add", classes.AgiRateAdd);
                    position++;
                    index++;
                }
            }

            // Intelligence attack rate
            if (classes.CheckIntRate())
            {
                if (classes.IntRateMul != 0)
                {
                    script += genHashSingle("IntRate_Mul", classes.IntRateMul);
                    position++;
                    index++;
                }
                if (classes.IntRateAdd != 0)
                {
                    script += genHashSingle("IntRate_Add", classes.IntRateAdd);
                    position++;
                    index++;
                }
            }

            // Physical defense attack rate
            if (classes.CheckPDefRate())
            {
                if (classes.PDefRateMul != 0)
                {
                    script += genHashSingle("PDefRate_Mul", classes.PDefRateMul);
                    position++;
                    index++;
                }
                if (classes.PDefRateAdd != 0)
                {
                    script += genHashSingle("PDefRate_Add", classes.PDefRateAdd);
                    position++;
                    index++;
                }
            }

            // Magical defense attack rate
            if (classes.CheckMDefRate())
            {
                if (classes.MDefRateMul != 0)
                {
                    script += genHashSingle("MDefRate_Mul", classes.MDefRateMul);
                    position++;
                    index++;
                }
                if (classes.MDefRateAdd != 0)
                {
                    script += genHashSingle("MDefRate_Add", classes.MDefRateAdd);
                    position++;
                    index++;
                }
            }

            // Guard rate
            if (classes.CheckGuardRate())
            {
                if (classes.GuardRateMul != 0)
                {
                    script += genHashSingle("GuardRate_Mul", classes.GuardRateMul);
                    position++;
                    index++;
                }
                if (classes.GuardRateAdd != 0)
                {
                    script += genHashSingle("GuardRate_Add", classes.GuardRateAdd);
                    position++;
                    index++;
                }
            }

            // Evasion rate
            if (classes.CheckEvaRate())
            {
                if (classes.EvaRateMul != 0)
                {
                    script += genHashSingle("EvaRate_Mul", classes.EvaRateMul);
                    position++;
                    index++;
                }
                if (classes.EvaRateAdd != 0)
                {
                    script += genHashSingle("EvaRate_Add", classes.EvaRateAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Defense against Attack Critical

            // Defense against attack critical rate
            if (classes.CheckDefCritRate())
            {
                if (classes.DefCritRateMul != 0)
                {
                    script += genHashSingle("Def_CritRate_Mul", classes.DefCritRateMul);
                    position++;
                    index++;
                }
                if (classes.DefCritRateAdd != 0)
                {
                    script += genHashSingle("Def_CritRate_Add", classes.DefCritRateAdd);
                    position++;
                    index++;
                }
            }

            // Defense against attack critical damage
            if (classes.CheckDefCritDamage())
            {
                if (classes.DefCritDamageMul != 0)
                {
                    script += genHashSingle("Def_CritDamage_Mul", classes.DefCritDamageMul);
                    position++;
                    index++;
                }
                if (classes.DefCritDamageAdd != 0)
                {
                    script += genHashSingle("Def_CritDamage_Add", classes.DefCritDamageAdd);
                    position++;
                    index++;
                }
            }

            // Defense against attack special critical rate
            if (classes.CheckDefSpCritRate())
            {
                if (classes.DefSpCritRateMul != 0)
                {
                    script += genHashSingle("Def_SpCritRate_Mul", classes.DefSpCritRateMul);
                    position++;
                    index++;
                }
                if (classes.DefSpCritRateAdd != 0)
                {
                    script += genHashSingle("Def_SpCritRate_Add", classes.DefSpCritRateAdd);
                    position++;
                    index++;
                }
            }

            // Strengh
            if (classes.CheckDefSpCritDamage())
            {
                if (classes.DefSpCritDamageMul != 0)
                {
                    script += genHashSingle("Def_SpCritDamage_Mul", classes.DefSpCritDamageMul);
                    position++;
                    index++;
                }
                if (classes.DefSpCritDamageAdd != 0)
                {
                    script += genHashSingle("Def_SpCritDamage_Add", classes.DefSpCritDamageAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Defense against Skill Critical

            // Defense against skill critical rate
            if (classes.CheckDefSkillCritRate())
            {
                if (classes.DefSkillCritRateMul != 0)
                {
                    script += genHashSingle("Def_Skill_CritRate_Mul", classes.DefSkillCritRateMul);
                    position++;
                    index++;
                }
                if (classes.DefSkillCritRateAdd != 0)
                {
                    script += genHashSingle("Def_Skill_CritRate_Add", classes.DefSkillCritRateAdd);
                    position++;
                    index++;
                }
            }

            // Defense against skill critical damage
            if (classes.CheckDefSkillCritDamage())
            {
                if (classes.DefSkillCritDamageMul != 0)
                {
                    script += genHashSingle("Def_Skill_CritDamage_Mul", classes.DefSkillCritDamageMul);
                    position++;
                    index++;
                }
                if (classes.DefSkillCritDamageAdd != 0)
                {
                    script += genHashSingle("Def_Skill_CritDamage_Add", classes.DefSkillCritDamageAdd);
                    position++;
                    index++;
                }
            }

            // Defense against skill special critical rate
            if (classes.CheckDefSkillSpCritRate())
            {
                if (classes.DefSkillSpCritRateMul != 0)
                {
                    script += genHashSingle("Def_Skill_SpCritRate_Mul", classes.DefSkillSpCritRateMul);
                    position++;
                    index++;
                }
                if (classes.DefSkillSpCritRateAdd != 0)
                {
                    script += genHashSingle("Def_Skill_SpCritRate_Add", classes.DefSkillSpCritRateAdd);
                    position++;
                    index++;
                }
            }

            // Defense against skill special critical damage
            if (classes.CheckDefSkillSpCritDamage())
            {
                if (classes.DefSkillSpCritDamageMul != 0)
                {
                    script += genHashSingle("Def_Skill_SpCritDamage_Mul", classes.DefSkillSpCritDamageMul);
                    position++;
                    index++;
                }
                if (classes.DefSkillSpCritDamageAdd != 0)
                {
                    script += genHashSingle("Def_Skill_SpCritDamage_Add", classes.DefSkillSpCritDamageAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Passive attack

            // Attack
            if (classes.CheckPassiveAtk())
            {
                if (classes.AtkMul != 0)
                {
                    script += genHashSingle("Atk_Mul", classes.AtkMul);
                    position++;
                    index++;
                }
                if (classes.AtkAdd != 0)
                {
                    script += genHashSingle("Atk_Add", classes.AtkAdd);
                    position++;
                    index++;
                }
            }

            // Hit rate
            if (classes.CheckPassiveHit())
            {
                if (classes.HitMul != 0)
                {
                    script += genHashSingle("HitRate_Mul", classes.HitMul);
                    position++;
                    index++;
                }
                if (classes.HitAdd != 0)
                {
                    script += genHashSingle("HitRate_Add", classes.HitAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Passive critical

            // Critical rate
            if (classes.CheckPassiveCritRate())
            {
                if (classes.CritRateMul != 0)
                {
                    script += genHashSingle("CritRate_Mul", classes.CritRateMul);
                    position++;
                    index++;
                }
                if (classes.CritRateAdd != 0)
                {
                    script += genHashSingle("CritRate_Add", classes.CritRateAdd);
                    position++;
                    index++;
                }
            }

            // Critical damage
            if (classes.CheckPassiveCritDamage())
            {
                if (classes.CritDamageMul != 0)
                {
                    script += genHashSingle("CritDamage_Mul", classes.CritDamageMul);
                    position++;
                    index++;
                }
                if (classes.CritDamageAdd != 0)
                {
                    script += genHashSingle("CritDamage_Add", classes.CritDamageAdd);
                    position++;
                    index++;
                }
            }

            // Critical guard rate reduction
            if (classes.CheckPassiveCritDefGuard())
            {
                if (classes.CritDefGuardMul != 0)
                {
                    script += genHashSingle("Crit_DefGuard_Mul", classes.CritDefGuardMul);
                    position++;
                    index++;
                }
                if (classes.CritDefGuardAdd != 0)
                {
                    script += genHashSingle("Crit_DefGuard_Add", classes.CritDefGuardAdd);
                    position++;
                    index++;
                }
            }

            // Critical evasion rate reduction
            if (classes.CheckPassiveCritDefEva())
            {
                if (classes.CritDefEvaMul != 0)
                {
                    script += genHashSingle("Crit_DefEva_Mul", classes.CritDefEvaMul);
                    position++;
                    index++;
                }
                if (classes.CritDefEvaAdd != 0)
                {
                    script += genHashSingle("Crit_DefEva_Add", classes.CritDefEvaAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Passive special critical

            // Special critical rate
            if (classes.CheckPassiveSpCritRate())
            {
                if (classes.SpCritRateMul != 0)
                {
                    script += genHashSingle("SpCritRate_Mul", classes.SpCritRateMul);
                    position++;
                    index++;
                }
                if (classes.SpCritRateAdd != 0)
                {
                    script += genHashSingle("SpCritRate_Add", classes.SpCritRateAdd);
                    position++;
                    index++;
                }
            }

            // Special critical damage
            if (classes.CheckPassiveSpCritDamage())
            {
                if (classes.SpCritDamageMul != 0)
                {
                    script += genHashSingle("SpCritDamage_Mul", classes.SpCritDamageMul);
                    position++;
                    index++;
                }
                if (classes.SpCritDamageAdd != 0)
                {
                    script += genHashSingle("SpCritDamage_Add", classes.SpCritDamageAdd);
                    position++;
                    index++;
                }
            }

            // Special critical guard rate reduction
            if (classes.CheckPassiveSpCritDefGuard())
            {
                if (classes.SpCritDefGuardMul != 0)
                {
                    script += genHashSingle("SpCrit_DefGuard_Mul", classes.SpCritDefGuardMul);
                    position++;
                    index++;
                }
                if (classes.SpCritDefGuardAdd != 0)
                {
                    script += genHashSingle("SpCrit_DefGuard_Add", classes.SpCritDefGuardAdd);
                    position++;
                    index++;
                }
            }

            // Special critical evasion rate reduction
            if (classes.CheckPassiveSpCritDefEva())
            {
                if (classes.SpCritDefEvaMul != 0)
                {
                    script += genHashSingle("SpCrit_DefEva_Mul", classes.SpCritDefEvaMul);
                    position++;
                    index++;
                }
                if (classes.SpCritDefEvaAdd != 0)
                {
                    script += genHashSingle("SpCrit_DefEva_Add", classes.SpCritDefEvaAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Passive defense

            // Physical defense
            if (classes.CheckPassivePDef())
            {
                if (classes.PDefMul != 0)
                {
                    script += genHashSingle("PDef_Mul", classes.PDefMul);
                    position++;
                    index++;
                }
                if (classes.PDefAdd != 0)
                {
                    script += genHashSingle("PDef_Add", classes.PDefAdd);
                    position++;
                    index++;
                }
            }

            // Magical defense
            if (classes.CheckPassiveMDef())
            {
                if (classes.MDefMul != 0)
                {
                    script += genHashSingle("MDef_Mul", classes.MDefMul);
                    position++;
                    index++;
                }
                if (classes.MDefAdd != 0)
                {
                    script += genHashSingle("MDef_Add", classes.MDefAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Unarmed Attack

            // Unarmed attack
            if (classes.CustomUnarmedAtk && (classes.AtkInitial != cfg.Default.AtkInitial || classes.AtkFinal != cfg.Default.AtkFinal))
            {
                script += genHashDouble("Atk", classes.AtkInitial, classes.AtkFinal);
                position++;
                index++;
            }

            // Unarmed hit rate
            if (classes.CustomUnarmedHit && (classes.HitInitial != cfg.Default.HitInitial || classes.HitFinal != cfg.Default.HitFinal))
            {
                script += genHashDouble("HitRate", classes.HitInitial * 100, classes.HitFinal * 100);
                position++;
                index++;
            }

            // Unarmed attack animation
            if (classes.CustomUnarmedAnim && (classes.AnimCaster != cfg.Default.AnimCaster || classes.AnimTarget != cfg.Default.AnimTarget))
            {
                script += genHashDouble("Anim", classes.AnimCaster, classes.AnimTarget);
                position++;
                index++;
            }

            // Unarmed strengh attack force
            if (classes.CustomUnarmedParamForce)
            {
                if (classes.StrForce != cfg.Default.StrForce)
                {
                    script += genHashSingle("Str_Force", classes.StrForce);
                    position++;
                    index++;
                }

                // Unarmed dexterity attack force
                if (classes.DexForce != cfg.Default.DexForce)
                {
                    script += genHashSingle("Dex_Force", classes.DexForce);
                    position++;
                    index++;
                }

                // Unarmed agility attack force
                if (classes.AgiForce != cfg.Default.AgiForce)
                {
                    script += genHashSingle("Agi_Force", classes.AgiForce);
                    position++;
                    index++;
                }

                // Unarmed intelligence attack force
                if (classes.IntForce != cfg.Default.IntForce)
                {
                    script += genHashSingle("Int_Force", classes.IntForce);
                    position++;
                    index++;
                }
            }

            // Unarmed physical defense attack force
            if (classes.CustomUnarmedDefenseForce)
            {
                if (classes.PDefForce != cfg.Default.PDefForce)
                {
                    script += genHashSingle("PDef_Force", classes.PDefForce);
                    position++;
                    index++;
                }

                // Unarmed magical defense attack force
                if (classes.MDefForce != cfg.Default.MDefForce)
                {
                    script += genHashSingle("MDef_Force", classes.MDefForce);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Unarmed Critical Rate

            // Unarmed critical rate
            if (classes.CustomUnarmedCritRate && classes.CritRate != cfg.Default.CritRate)
            {
                script += genHashSingle("CritRate", classes.CritRate * 100);
                position++;
                index++;
            }

            // Unarmed critical damage
            if (classes.CustomUnarmedCritDamage && classes.CritDamage != cfg.Default.CritDamage)
            {
                script += genHashSingle("CritDamage", classes.CritDamage);
                position++;
                index++;
            }

            // Unarmed critical guard rate reduction
            if (classes.CustomUnarmedCritDefGuard && classes.CritDefGuard != cfg.Default.CritDefGuard)
            {
                script += genHashSingle("Crit_DefGuard", classes.CritDefGuard);
                position++;
                index++;
            }

            // Unarmed critical evasion rate reduction
            if (classes.CustomUnarmedCritDefEva && classes.CritDefEva != cfg.Default.CritDefEva)
            {
                script += genHashSingle("Crit_DefEva", classes.CritDefEva);
                position++;
                index++;
            }

            #endregion

            #region Unarmed Special Critical Rate

            // Unarmed special critical rate
            if (classes.CustomUnarmedSpCritRate && classes.SpCritRate != cfg.Default.SpCritRate)
            {
                script += genHashSingle("SpCritRate", classes.SpCritRate * 100);
                position++;
                index++;
            }

            // Unarmed special critical damage
            if (classes.CustomUnarmedSpCritDamage && classes.SpCritDamage != cfg.Default.SpCritDamage)
            {
                script += genHashSingle("SpCritDamage", classes.SpCritDamage);
                position++;
                index++;
            }

            // Unarmed special critical guard rate reduction
            if (classes.CustomUnarmedSpCritDefGuard && classes.SpCritDefGuard != cfg.Default.SpCritDefGuard)
            {
                script += genHashSingle("SpCrit_DefGuard", classes.SpCritDefGuard);
                position++;
                index++;
            }

            // Unarmed special critical evasion rate reduction
            if (classes.CustomUnarmedSpCritDefEva && classes.SpCritDefEva != cfg.Default.SpCritDefEva)
            {
                script += genHashSingle("SpCrit_DefEva", classes.SpCritDefEva);
                position++;
                index++;
            }

            #endregion

            #region Unarmoured Defense

            // Unarmoured Physical Defense
            if (classes.CustomUnarmouredPDef && (classes.PDefInitial != cfg.Default.PDefInitial || classes.PDefFinal != cfg.Default.PDefFinal))
            {
                script += genHashDouble("PDef", classes.PDefInitial, classes.PDefFinal);
                position++;
                index++;
            }

            // Unarmoured Magical Defense
            if (classes.CustomUnarmouredMDef && (classes.MDefInitial != cfg.Default.MDefInitial || classes.MDefFinal != cfg.Default.MDefFinal))
            {
                script += genHashDouble("MDef", classes.MDefInitial, classes.MDefFinal);
                position++;
                index++;
            }

            #endregion

            return script;
        }

        #endregion

        #region Skill

        private static string genDataPackSkill(DataPackSkill skill)
        {
            index = 0;
            position = 0;
            column = 3;

            string script = "";

            #region Attack

            // Attack
            if (skill.CustomAtk && skill.AtkInitial != cfg.Default.AtkInitial)
            {
                script += genHashSingle("Atk", skill.AtkInitial);
                position++;
                index++;
            }

            // Hit rate
            if (skill.CustomHit && skill.HitInitial != cfg.Default.HitInitial)
            {
                script += genHashSingle("HitRate", skill.HitInitial * 100);
                position++;
                index++;
            }

            // Strengh attack force
            if (skill.CustomParamForce)
            {
                if (skill.StrForce != cfg.Default.StrForce)
                {
                    script += genHashSingle("Str_Force", skill.StrForce);
                    position++;
                    index++;
                }

                // Dexterity attack force
                if (skill.DexForce != cfg.Default.DexForce)
                {
                    script += genHashSingle("Dex_Force", skill.DexForce);
                    position++;
                    index++;
                }

                // Agility attack force
                if (skill.AgiForce != cfg.Default.AgiForce)
                {
                    script += genHashSingle("Agi_Force", skill.AgiForce);
                    position++;
                    index++;
                }

                // Intelligence attack force
                if (skill.IntForce != cfg.Default.IntForce)
                {
                    script += genHashSingle("Int_Force", skill.IntForce);
                    position++;
                    index++;
                }
            }

            // Physical defense attack force
            if (skill.CustomDefenseForce)
            {
                if (skill.PDefForce != cfg.Default.PDefForce)
                {
                    script += genHashSingle("PDef_Force", skill.PDefForce);
                    position++;
                    index++;
                }

                // Magical defense attack force
                if (skill.MDefForce != cfg.Default.MDefForce)
                {
                    script += genHashSingle("MDef_Force", skill.MDefForce);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Critical Rate

            // Critical rate
            if (skill.CustomCritRate && skill.CritRate != cfg.Default.CritRate)
            {
                script += genHashSingle("CritRate", skill.CritRate * 100);
                position++;
                index++;
            }

            // Critical damage
            if (skill.CustomCritDamage && skill.CritDamage != cfg.Default.CritDamage)
            {
                script += genHashSingle("CritDamage", skill.CritDamage);
                position++;
                index++;
            }

            // Critical guard rate reduction
            if (skill.CustomCritDefGuard && skill.CritDefGuard != cfg.Default.CritDefGuard)
            {
                script += genHashSingle("Crit_DefGuard", skill.CritDefGuard);
                position++;
                index++;
            }

            // Critical evasion rate reduction
            if (skill.CustomCritDefEva && skill.CritDefEva != cfg.Default.CritDefEva)
            {
                script += genHashSingle("Crit_DefEva", skill.CritDefEva);
                position++;
                index++;
            }

            #endregion

            #region Special Critical Rate

            // Special critical rate
            if (skill.CustomSpCritRate && skill.SpCritRate != cfg.Default.SpCritRate)
            {
                script += genHashSingle("SpCritRate", skill.SpCritRate * 100);
                position++;
                index++;
            }

            // Special critical damage
            if (skill.CustomSpCritDamage && skill.SpCritDamage != cfg.Default.SpCritDamage)
            {
                script += genHashSingle("SpCritDamage", skill.SpCritDamage);
                position++;
                index++;
            }

            // Special critical guard rate reduction
            if (skill.CustomSpCritDefGuard && skill.SpCritDefGuard != cfg.Default.SpCritDefGuard)
            {
                script += genHashSingle("SpCrit_DefGuard", skill.SpCritDefGuard);
                position++;
                index++;
            }

            // Special critical evasion rate reduction
            if (skill.CustomSpCritDefEva && skill.SpCritDefEva != cfg.Default.SpCritDefEva)
            {
                script += genHashSingle("SpCrit_DefEva", skill.SpCritDefEva);
                position++;
                index++;
            }


            #endregion

            return script;
        }

        #endregion

        #region Passive Skill

        private static string genDataPackPassiveSkill(DataPackPassiveSkill passiveSkill)
        {
            index = 0;
            position = 0;
            column = 3;

            string script = "";

            #region Parameter

            // Maximum HP
            if (passiveSkill.CheckMaxHP())
            {
                if (passiveSkill.MaxHPMul != 0)
                {
                    script += genHashSingle("MaxHP_Mul", passiveSkill.MaxHPMul);
                    position++;
                    index++;
                }
                if (passiveSkill.MaxHPAdd != 0)
                {
                    script += genHashSingle("MaxHP_Add", passiveSkill.MaxHPAdd);
                    position++;
                    index++;
                }
            }

            // Maximum HP
            if (passiveSkill.CheckMaxSP())
            {
                if (passiveSkill.MaxSPMul != 0)
                {
                    script += genHashSingle("MaxSP_Mul", passiveSkill.MaxSPMul);
                    position++;
                    index++;
                }
                if (passiveSkill.MaxSPAdd != 0)
                {
                    script += genHashSingle("MaxSP_Add", passiveSkill.MaxSPAdd);
                    position++;
                    index++;
                }
            }

            // Strengh
            if (passiveSkill.CheckStr())
            {
                if (passiveSkill.StrMul != 0)
                {
                    script += genHashSingle("Str_Mul", passiveSkill.StrMul);
                    position++;
                    index++;
                }
                if (passiveSkill.StrAdd != 0)
                {
                    script += genHashSingle("Str_Add", passiveSkill.StrAdd);
                    position++;
                    index++;
                }
            }

            // Dexterity
            if (passiveSkill.CheckDex())
            {
                if (passiveSkill.DexMul != 0)
                {
                    script += genHashSingle("Dex_Mul", passiveSkill.DexMul);
                    position++;
                    index++;
                }
                if (passiveSkill.DexAdd != 0)
                {
                    script += genHashSingle("Dex_Add", passiveSkill.DexAdd);
                    position++;
                    index++;
                }
            }

            // Agility
            if (passiveSkill.CheckAgi())
            {
                if (passiveSkill.AgiMul != 0)
                {
                    script += genHashSingle("Agi_Mul", passiveSkill.AgiMul);
                    position++;
                    index++;
                }
                if (passiveSkill.AgiAdd != 0)
                {
                    script += genHashSingle("Agi_Add", passiveSkill.AgiAdd);
                    position++;
                    index++;
                }
            }

            // Intelligence
            if (passiveSkill.CheckInt())
            {
                if (passiveSkill.IntMul != 0)
                {
                    script += genHashSingle("Int_Mul", passiveSkill.IntMul);
                    position++;
                    index++;
                }
                if (passiveSkill.IntAdd != 0)
                {
                    script += genHashSingle("Int_Add", passiveSkill.IntAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Parameter Rate

            // Intelligence
            if (passiveSkill.CheckStrRate())
            {
                if (passiveSkill.StrRateMul != 0)
                {
                    script += genHashSingle("StrRate_Mul", passiveSkill.StrRateMul);
                    position++;
                    index++;
                }
                if (passiveSkill.StrRateAdd != 0)
                {
                    script += genHashSingle("StrRate_Add", passiveSkill.StrRateAdd);
                    position++;
                    index++;
                }
            }

            // Dexterity attack rate
            if (passiveSkill.CheckDexRate())
            {
                if (passiveSkill.DexRateMul != 0)
                {
                    script += genHashSingle("DexRate_Mul", passiveSkill.DexRateMul);
                    position++;
                    index++;
                }
                if (passiveSkill.DexRateAdd != 0)
                {
                    script += genHashSingle("DexRate_Add", passiveSkill.DexRateAdd);
                    position++;
                    index++;
                }
            }

            // Agility attack rate
            if (passiveSkill.CheckAgiRate())
            {
                if (passiveSkill.AgiRateMul != 0)
                {
                    script += genHashSingle("AgiRate_Mul", passiveSkill.AgiRateMul);
                    position++;
                    index++;
                }
                if (passiveSkill.AgiRateAdd != 0)
                {
                    script += genHashSingle("AgiRate_Add", passiveSkill.AgiRateAdd);
                    position++;
                    index++;
                }
            }

            // Intelligence attack rate
            if (passiveSkill.CheckIntRate())
            {
                if (passiveSkill.IntRateMul != 0)
                {
                    script += genHashSingle("IntRate_Mul", passiveSkill.IntRateMul);
                    position++;
                    index++;
                }
                if (passiveSkill.IntRateAdd != 0)
                {
                    script += genHashSingle("IntRate_Add", passiveSkill.IntRateAdd);
                    position++;
                    index++;
                }
            }

            // Physical defense attack rate
            if (passiveSkill.CheckPDefRate())
            {
                if (passiveSkill.PDefRateMul != 0)
                {
                    script += genHashSingle("PDefRate_Mul", passiveSkill.PDefRateMul);
                    position++;
                    index++;
                }
                if (passiveSkill.PDefRateAdd != 0)
                {
                    script += genHashSingle("PDefRate_Add", passiveSkill.PDefRateAdd);
                    position++;
                    index++;
                }
            }

            // Magical defense attack rate
            if (passiveSkill.CheckMDefRate())
            {
                if (passiveSkill.MDefRateMul != 0)
                {
                    script += genHashSingle("MDefRate_Mul", passiveSkill.MDefRateMul);
                    position++;
                    index++;
                }
                if (passiveSkill.MDefRateAdd != 0)
                {
                    script += genHashSingle("MDefRate_Add", passiveSkill.MDefRateAdd);
                    position++;
                    index++;
                }
            }

            // Guard rate
            if (passiveSkill.CheckGuardRate())
            {
                if (passiveSkill.GuardRateMul != 0)
                {
                    script += genHashSingle("GuardRate_Mul", passiveSkill.GuardRateMul);
                    position++;
                    index++;
                }
                if (passiveSkill.GuardRateAdd != 0)
                {
                    script += genHashSingle("GuardRate_Add", passiveSkill.GuardRateAdd);
                    position++;
                    index++;
                }
            }

            // Evasion rate
            if (passiveSkill.CheckEvaRate())
            {
                if (passiveSkill.EvaRateMul != 0)
                {
                    script += genHashSingle("EvaRate_Mul", passiveSkill.EvaRateMul);
                    position++;
                    index++;
                }
                if (passiveSkill.EvaRateAdd != 0)
                {
                    script += genHashSingle("EvaRate_Add", passiveSkill.EvaRateAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Defense against Attack Critical

            // Defense against attack critical rate
            if (passiveSkill.CheckDefCritRate())
            {
                if (passiveSkill.DefCritRateMul != 0)
                {
                    script += genHashSingle("Def_CritRate_Mul", passiveSkill.DefCritRateMul);
                    position++;
                    index++;
                }
                if (passiveSkill.DefCritRateAdd != 0)
                {
                    script += genHashSingle("Def_CritRate_Add", passiveSkill.DefCritRateAdd);
                    position++;
                    index++;
                }
            }

            // Defense against attack critical damage
            if (passiveSkill.CheckDefCritDamage())
            {
                if (passiveSkill.DefCritDamageMul != 0)
                {
                    script += genHashSingle("Def_CritDamage_Mul", passiveSkill.DefCritDamageMul);
                    position++;
                    index++;
                }
                if (passiveSkill.DefCritDamageAdd != 0)
                {
                    script += genHashSingle("Def_CritDamage_Add", passiveSkill.DefCritDamageAdd);
                    position++;
                    index++;
                }
            }

            // Defense against attack special critical rate
            if (passiveSkill.CheckDefSpCritRate())
            {
                if (passiveSkill.DefSpCritRateMul != 0)
                {
                    script += genHashSingle("Def_SpCritRate_Mul", passiveSkill.DefSpCritRateMul);
                    position++;
                    index++;
                }
                if (passiveSkill.DefSpCritRateAdd != 0)
                {
                    script += genHashSingle("Def_SpCritRate_Add", passiveSkill.DefSpCritRateAdd);
                    position++;
                    index++;
                }
            }

            // Strengh
            if (passiveSkill.CheckDefSpCritDamage())
            {
                if (passiveSkill.DefSpCritDamageMul != 0)
                {
                    script += genHashSingle("Def_SpCritDamage_Mul", passiveSkill.DefSpCritDamageMul);
                    position++;
                    index++;
                }
                if (passiveSkill.DefSpCritDamageAdd != 0)
                {
                    script += genHashSingle("Def_SpCritDamage_Add", passiveSkill.DefSpCritDamageAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Defense against Skill Critical

            // Defense against skill critical rate
            if (passiveSkill.CheckDefSkillCritRate())
            {
                if (passiveSkill.DefSkillCritRateMul != 0)
                {
                    script += genHashSingle("Def_Skill_CritRate_Mul", passiveSkill.DefSkillCritRateMul);
                    position++;
                    index++;
                }
                if (passiveSkill.DefSkillCritRateAdd != 0)
                {
                    script += genHashSingle("Def_Skill_CritRate_Add", passiveSkill.DefSkillCritRateAdd);
                    position++;
                    index++;
                }
            }

            // Defense against skill critical damage
            if (passiveSkill.CheckDefSkillCritDamage())
            {
                if (passiveSkill.DefSkillCritDamageMul != 0)
                {
                    script += genHashSingle("Def_Skill_CritDamage_Mul", passiveSkill.DefSkillCritDamageMul);
                    position++;
                    index++;
                }
                if (passiveSkill.DefSkillCritDamageAdd != 0)
                {
                    script += genHashSingle("Def_Skill_CritDamage_Add", passiveSkill.DefSkillCritDamageAdd);
                    position++;
                    index++;
                }
            }

            // Defense against skill special critical rate
            if (passiveSkill.CheckDefSkillSpCritRate())
            {
                if (passiveSkill.DefSkillSpCritRateMul != 0)
                {
                    script += genHashSingle("Def_Skill_SpCritRate_Mul", passiveSkill.DefSkillSpCritRateMul);
                    position++;
                    index++;
                }
                if (passiveSkill.DefSkillSpCritRateAdd != 0)
                {
                    script += genHashSingle("Def_Skill_SpCritRate_Add", passiveSkill.DefSkillSpCritRateAdd);
                    position++;
                    index++;
                }
            }

            // Defense against skill special critical damage
            if (passiveSkill.CheckDefSkillSpCritDamage())
            {
                if (passiveSkill.DefSkillSpCritDamageMul != 0)
                {
                    script += genHashSingle("Def_Skill_SpCritDamage_Mul", passiveSkill.DefSkillSpCritDamageMul);
                    position++;
                    index++;
                }
                if (passiveSkill.DefSkillSpCritDamageAdd != 0)
                {
                    script += genHashSingle("Def_Skill_SpCritDamage_Add", passiveSkill.DefSkillSpCritDamageAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Attack

            // Attack
            if (passiveSkill.CheckAtk())
            {
                if (passiveSkill.AtkMul != 0)
                {
                    script += genHashSingle("Atk_Mul", passiveSkill.AtkMul);
                    position++;
                    index++;
                }
                if (passiveSkill.AtkAdd != 0)
                {
                    script += genHashSingle("Atk_Add", passiveSkill.AtkAdd);
                    position++;
                    index++;
                }
            }

            // Hit rate
            if (passiveSkill.CheckHit())
            {
                if (passiveSkill.HitMul != 0)
                {
                    script += genHashSingle("HitRate_Mul", passiveSkill.HitMul);
                    position++;
                    index++;
                }
                if (passiveSkill.HitAdd != 0)
                {
                    script += genHashSingle("HitRate_Add", passiveSkill.HitAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Critical

            // Critical rate
            if (passiveSkill.CheckCritRate())
            {
                if (passiveSkill.CritRateMul != 0)
                {
                    script += genHashSingle("CritRate_Mul", passiveSkill.CritRateMul);
                    position++;
                    index++;
                }
                if (passiveSkill.CritRateAdd != 0)
                {
                    script += genHashSingle("CritRate_Add", passiveSkill.CritRateAdd);
                    position++;
                    index++;
                }
            }

            // Critical damage
            if (passiveSkill.CheckCritDamage())
            {
                if (passiveSkill.CritDamageMul != 0)
                {
                    script += genHashSingle("CritDamage_Mul", passiveSkill.CritDamageMul);
                    position++;
                    index++;
                }
                if (passiveSkill.CritDamageAdd != 0)
                {
                    script += genHashSingle("CritDamage_Add", passiveSkill.CritDamageAdd);
                    position++;
                    index++;
                }
            }

            // Critical guard rate reduction
            if (passiveSkill.CheckCritDefGuard())
            {
                if (passiveSkill.CritDefGuardMul != 0)
                {
                    script += genHashSingle("Crit_DefGuard_Mul", passiveSkill.CritDefGuardMul);
                    position++;
                    index++;
                }
                if (passiveSkill.CritDefGuardAdd != 0)
                {
                    script += genHashSingle("Crit_DefGuard_Add", passiveSkill.CritDefGuardAdd);
                    position++;
                    index++;
                }
            }

            // Critical evasion rate reduction
            if (passiveSkill.CheckCritDefEva())
            {
                if (passiveSkill.CritDefEvaMul != 0)
                {
                    script += genHashSingle("Crit_DefEva_Mul", passiveSkill.CritDefEvaMul);
                    position++;
                    index++;
                }
                if (passiveSkill.CritDefEvaAdd != 0)
                {
                    script += genHashSingle("Crit_DefEva_Add", passiveSkill.CritDefEvaAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Special Critical

            // Special critical rate
            if (passiveSkill.CheckSpCritRate())
            {
                if (passiveSkill.SpCritRateMul != 0)
                {
                    script += genHashSingle("SpCritRate_Mul", passiveSkill.SpCritRateMul);
                    position++;
                    index++;
                }
                if (passiveSkill.SpCritRateAdd != 0)
                {
                    script += genHashSingle("SpCritRate_Add", passiveSkill.SpCritRateAdd);
                    position++;
                    index++;
                }
            }

            // Special critical damage
            if (passiveSkill.CheckSpCritDamage())
            {
                if (passiveSkill.SpCritDamageMul != 0)
                {
                    script += genHashSingle("SpCritDamage_Mul", passiveSkill.SpCritDamageMul);
                    position++;
                    index++;
                }
                if (passiveSkill.SpCritDamageAdd != 0)
                {
                    script += genHashSingle("SpCritDamage_Add", passiveSkill.SpCritDamageAdd);
                    position++;
                    index++;
                }
            }

            // Special critical guard rate reduction
            if (passiveSkill.CheckSpCritDefGuard())
            {
                if (passiveSkill.SpCritDefGuardMul != 0)
                {
                    script += genHashSingle("SpCrit_DefGuard_Mul", passiveSkill.SpCritDefGuardMul);
                    position++;
                    index++;
                }
                if (passiveSkill.SpCritDefGuardAdd != 0)
                {
                    script += genHashSingle("SpCrit_DefGuard_Add", passiveSkill.SpCritDefGuardAdd);
                    position++;
                    index++;
                }
            }

            // Special critical evasion rate reduction
            if (passiveSkill.CheckSpCritDefEva())
            {
                if (passiveSkill.SpCritDefEvaMul != 0)
                {
                    script += genHashSingle("SpCrit_DefEva_Mul", passiveSkill.SpCritDefEvaMul);
                    position++;
                    index++;
                }
                if (passiveSkill.SpCritDefEvaAdd != 0)
                {
                    script += genHashSingle("SpCrit_DefEva_Add", passiveSkill.SpCritDefEvaAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Defense

            // Physical defense
            if (passiveSkill.CheckPDef())
            {
                if (passiveSkill.PDefMul != 0)
                {
                    script += genHashSingle("PDef_Mul", passiveSkill.PDefMul);
                    position++;
                    index++;
                }
                if (passiveSkill.PDefAdd != 0)
                {
                    script += genHashSingle("PDef_Add", passiveSkill.PDefAdd);
                    position++;
                    index++;
                }
            }

            // Magical defense
            if (passiveSkill.CheckMDef())
            {
                if (passiveSkill.MDefMul != 0)
                {
                    script += genHashSingle("MDef_Mul", passiveSkill.MDefMul);
                    position++;
                    index++;
                }
                if (passiveSkill.MDefAdd != 0)
                {
                    script += genHashSingle("MDef_Add", passiveSkill.MDefAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            return script;
        }

        #endregion

        #region Weapon

        private static string genDataPackWeapon(DataPackEquipment equipment)
        {
            index = 0;
            position = 0;
            column = 3;

            string script = "";

            #region Equipment

            // Hand
            if (equipment.CheckHand())
            {
                script += genHashSingle("NumberHand", equipment.Hand);
                position++;
                index++;
            }

            // Shield Slot Only
            if (equipment.CheckShieldOnly())
            {
                script += genHashSingle("OffOnly", (equipment.ShieldOnly.ToString()).ToLower());
                position++;
                index++;
            }

            // Weapon Slot Only
            if (equipment.CheckWeaponOnly())
            {
                script += genHashSingle("MainOnly", (equipment.WeaponOnly.ToString()).ToLower());
                position++;
                index++;
            }

            // Cursed
            if (equipment.CheckCursed())
            {
                script += genHashSingle("Cursed", (equipment.Cursed.ToString()).ToLower());
                position++;
                index++;
            }

            // Switch ID
            if (equipment.CheckSwitchID())
            {
                script += genHashSingle("SwitchID", equipment.SwitchID.ToString());
                position++;
                index++;
            }

            #endregion

            #region Attack

            // Attack
            if (equipment.CustomAtk && equipment.AtkInitial != cfg.Default.AtkInitial)
            {
                script += genHashSingle("Atk", equipment.AtkInitial);
                position++;
                index++;
            }

            // Hit rate
            if (equipment.CustomHit && equipment.HitInitial != cfg.Default.HitInitial)
            {
                script += genHashSingle("HitRate", equipment.HitInitial * 100);
                position++;
                index++;
            }

            // Strengh attack force
            if (equipment.CustomParamForce)
            {
                if (equipment.StrForce != cfg.Default.StrForce)
                {
                    script += genHashSingle("Str_Force", equipment.StrForce);
                    position++;
                    index++;
                }

                // Dexterity attack force
                if (equipment.DexForce != cfg.Default.DexForce)
                {
                    script += genHashSingle("Dex_Force", equipment.DexForce);
                    position++;
                    index++;
                }

                // Agility attack force
                if (equipment.AgiForce != cfg.Default.AgiForce)
                {
                    script += genHashSingle("Agi_Force", equipment.AgiForce);
                    position++;
                    index++;
                }

                // Intelligence attack force
                if (equipment.IntForce != cfg.Default.IntForce)
                {
                    script += genHashSingle("Int_Force", equipment.IntForce);
                    position++;
                    index++;
                }
            }

            // Physical defense attack force
            if (equipment.CustomDefenseForce)
            {
                if (equipment.PDefForce != cfg.Default.PDefForce)
                {
                    script += genHashSingle("PDef_Force", equipment.PDefForce);
                    position++;
                    index++;
                }

                // Magical defense attack force
                if (equipment.MDefForce != cfg.Default.MDefForce)
                {
                    script += genHashSingle("MDef_Force", equipment.MDefForce);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Critical Rate

            // Critical rate
            if (equipment.CustomCritRate && equipment.CritRateInitial != cfg.Default.CritRate)
            {
                script += genHashSingle("CritRate", equipment.CritRateInitial * 100);
                position++;
                index++;
            }

            // Critical damage
            if (equipment.CustomCritDamage && equipment.CritDamageInitial != cfg.Default.CritDamage)
            {
                script += genHashSingle("CritDamage", equipment.CritDamageInitial);
                position++;
                index++;
            }

            // Critical guard rate reduction
            if (equipment.CustomCritDefGuard && equipment.CritDefGuardInitial != cfg.Default.CritDefGuard)
            {
                script += genHashSingle("Crit_DefGuard", equipment.CritDefGuardInitial);
                position++;
                index++;
            }

            // Critical evasion rate reduction
            if (equipment.CustomCritDefEva && equipment.CritDefEvaInitial != cfg.Default.CritDefEva)
            {
                script += genHashSingle("Crit_DefEva", equipment.CritDefEvaInitial);
                position++;
                index++;
            }

            #endregion

            #region Special Critical

            // Special critical rate
            if (equipment.CustomSpCritRate && equipment.SpCritRateInitial != cfg.Default.SpCritRate)
            {
                script += genHashSingle("SpCritRate", equipment.SpCritRateInitial * 100);
                position++;
                index++;
            }

            // Special critical damage
            if (equipment.CustomSpCritDamage && equipment.SpCritDamageInitial != cfg.Default.SpCritDamage)
            {
                script += genHashSingle("SpCritDamage", equipment.SpCritDamageInitial);
                position++;
                index++;
            }

            // Special critical guard rate reduction
            if (equipment.CustomSpCritDefGuard && equipment.SpCritDefGuardInitial != cfg.Default.SpCritDefGuard)
            {
                script += genHashSingle("SpCrit_DefGuard", equipment.SpCritDefGuardInitial);
                position++;
                index++;
            }

            // Special critical evasion rate reduction
            if (equipment.CustomSpCritDefEva && equipment.SpCritDefEvaInitial != cfg.Default.SpCritDefEva)
            {
                script += genHashSingle("SpCrit_DefEva", equipment.SpCritDefEvaInitial);
                position++;
                index++;
            }

            #endregion

            #region Parameter

            // Maximum HP
            if (equipment.CheckMaxHP())
            {
                if (equipment.MaxHPMul != 0)
                {
                    script += genHashSingle("MaxHP_Mul", equipment.MaxHPMul);
                    position++;
                    index++;
                }
                if (equipment.MaxHPAdd != 0)
                {
                    script += genHashSingle("MaxHP_Add", equipment.MaxHPAdd);
                    position++;
                    index++;
                }
            }

            // Maximum HP
            if (equipment.CheckMaxSP())
            {
                if (equipment.MaxSPMul != 0)
                {
                    script += genHashSingle("MaxSP_Mul", equipment.MaxSPMul);
                    position++;
                    index++;
                }
                if (equipment.MaxSPAdd != 0)
                {
                    script += genHashSingle("MaxSP_Add", equipment.MaxSPAdd);
                    position++;
                    index++;
                }
            }

            // Strengh
            if (equipment.CheckStr())
            {
                if (equipment.StrMul != 0)
                {
                    script += genHashSingle("Str_Mul", equipment.StrMul);
                    position++;
                    index++;
                }
                if (equipment.StrAdd != 0)
                {
                    script += genHashSingle("Str_Add", equipment.StrAdd);
                    position++;
                    index++;
                }
            }

            // Dexterity
            if (equipment.CheckDex())
            {
                if (equipment.DexMul != 0)
                {
                    script += genHashSingle("Dex_Mul", equipment.DexMul);
                    position++;
                    index++;
                }
                if (equipment.DexAdd != 0)
                {
                    script += genHashSingle("Dex_Add", equipment.DexAdd);
                    position++;
                    index++;
                }
            }

            // Agility
            if (equipment.CheckAgi())
            {
                if (equipment.AgiMul != 0)
                {
                    script += genHashSingle("Agi_Mul", equipment.AgiMul);
                    position++;
                    index++;
                }
                if (equipment.AgiAdd != 0)
                {
                    script += genHashSingle("Agi_Add", equipment.AgiAdd);
                    position++;
                    index++;
                }
            }

            // Intelligence
            if (equipment.CheckInt())
            {
                if (equipment.IntMul != 0)
                {
                    script += genHashSingle("Int_Mul", equipment.IntMul);
                    position++;
                    index++;
                }
                if (equipment.IntAdd != 0)
                {
                    script += genHashSingle("Int_Add", equipment.IntAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Parameter Rate

            // Intelligence
            if (equipment.CheckStrRate())
            {
                if (equipment.StrRateMul != 0)
                {
                    script += genHashSingle("StrRate_Mul", equipment.StrRateMul);
                    position++;
                    index++;
                }
                if (equipment.StrRateAdd != 0)
                {
                    script += genHashSingle("StrRate_Mul", equipment.StrRateAdd);
                    position++;
                    index++;
                }
            }

            // Dexterity attack rate
            if (equipment.CheckDexRate())
            {
                if (equipment.DexRateMul != 0)
                {
                    script += genHashSingle("DexRate_Mul", equipment.DexRateMul);
                    position++;
                    index++;
                }
                if (equipment.DexRateAdd != 0)
                {
                    script += genHashSingle("DexRate_Add", equipment.DexRateAdd);
                    position++;
                    index++;
                }
            }

            // Agility attack rate
            if (equipment.CheckAgiRate())
            {
                if (equipment.AgiRateMul != 0)
                {
                    script += genHashSingle("AgiRate_Mul", equipment.AgiRateMul);
                    position++;
                    index++;
                }
                if (equipment.AgiRateAdd != 0)
                {
                    script += genHashSingle("AgiRate_Mul", equipment.AgiRateAdd);
                    position++;
                    index++;
                }
            }

            // Intelligence attack rate
            if (equipment.CheckIntRate())
            {
                if (equipment.IntRateMul != 0)
                {
                    script += genHashSingle("IntRate_Mul", equipment.IntRateMul);
                    position++;
                    index++;
                }
                if (equipment.IntRateAdd != 0)
                {
                    script += genHashSingle("IntRate_Add", equipment.IntRateAdd);
                    position++;
                    index++;
                }
            }

            // Physical defense attack rate
            if (equipment.CheckPDefRate())
            {
                if (equipment.PDefRateMul != 0)
                {
                    script += genHashSingle("PDefRate_Mul", equipment.PDefRateMul);
                    position++;
                    index++;
                }
                if (equipment.PDefRateAdd != 0)
                {
                    script += genHashSingle("PDefRate_Add", equipment.PDefRateAdd);
                    position++;
                    index++;
                }
            }

            // Magical defense attack rate
            if (equipment.CheckMDefRate())
            {
                if (equipment.MDefRateMul != 0)
                {
                    script += genHashSingle("MDefRate_Mul", equipment.MDefRateMul);
                    position++;
                    index++;
                }
                if (equipment.MDefRateAdd != 0)
                {
                    script += genHashSingle("MDefRate_Add", equipment.MDefRateAdd);
                    position++;
                    index++;
                }
            }

            // Guard rate
            if (equipment.CheckGuardRate())
            {
                if (equipment.GuardRateMul != 0)
                {
                    script += genHashSingle("GuardRate_Mul", equipment.GuardRateMul);
                    position++;
                    index++;
                }
                if (equipment.GuardRateAdd != 0)
                {
                    script += genHashSingle("GuardRate_Add", equipment.GuardRateAdd);
                    position++;
                    index++;
                }
            }

            // Evasion rate
            if (equipment.CheckEvaRate())
            {
                if (equipment.EvaRateMul != 0)
                {
                    script += genHashSingle("EvaRate_Mul", equipment.EvaRateMul);
                    position++;
                    index++;
                }
                if (equipment.EvaRateAdd != 0)
                {
                    script += genHashSingle("EvaRate_Add", equipment.EvaRateAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Defense against Attack Critical

            // Defense against attack critical rate
            if (equipment.CheckDefCritRate())
            {
                if (equipment.DefCritRateMul != 0)
                {
                    script += genHashSingle("Def_CritRate_Mul", equipment.DefCritRateMul);
                    position++;
                    index++;
                }
                if (equipment.DefCritRateAdd != 0)
                {
                    script += genHashSingle("Def_CritRate_Add", equipment.DefCritRateAdd);
                    position++;
                    index++;
                }
            }

            // Defense against attack critical damage
            if (equipment.CheckDefCritDamage())
            {
                if (equipment.DefCritDamageMul != 0)
                {
                    script += genHashSingle("Def_CritDamage_Mul", equipment.DefCritDamageMul);
                    position++;
                    index++;
                }
                if (equipment.DefCritDamageAdd != 0)
                {
                    script += genHashSingle("Def_CritDamage_Add", equipment.DefCritDamageAdd);
                    position++;
                    index++;
                }
            }

            // Defense against attack special critical rate
            if (equipment.CheckDefSpCritRate())
            {
                if (equipment.DefSpCritRateMul != 0)
                {
                    script += genHashSingle("Def_SpCritRate_Mul", equipment.DefSpCritRateMul);
                    position++;
                    index++;
                }
                if (equipment.DefSpCritRateAdd != 0)
                {
                    script += genHashSingle("Def_SpCritRate_Add", equipment.DefSpCritRateAdd);
                    position++;
                    index++;
                }
            }

            // Strengh
            if (equipment.CheckDefSpCritDamage())
            {
                if (equipment.DefSpCritDamageMul != 0)
                {
                    script += genHashSingle("Def_SpCritDamage_Mul", equipment.DefSpCritDamageMul);
                    position++;
                    index++;
                }
                if (equipment.DefSpCritDamageAdd != 0)
                {
                    script += genHashSingle("Def_SpCritDamage_Add", equipment.DefSpCritDamageAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Defense against Skill Critical

            // Defense against skill critical rate
            if (equipment.CheckDefSkillCritRate())
            {
                if (equipment.DefSkillCritRateMul != 0)
                {
                    script += genHashSingle("Def_Skill_CritRate_Mul", equipment.DefSkillCritRateMul);
                    position++;
                    index++;
                }
                if (equipment.DefSkillCritRateAdd != 0)
                {
                    script += genHashSingle("Def_Skill_CritRate_Add", equipment.DefSkillCritRateAdd);
                    position++;
                    index++;
                }
            }

            // Defense against skill critical damage
            if (equipment.CheckDefSkillCritDamage())
            {
                if (equipment.DefSkillCritDamageMul != 0)
                {
                    script += genHashSingle("Def_Skill_CritDamage_Mul", equipment.DefSkillCritDamageMul);
                    position++;
                    index++;
                }
                if (equipment.DefSkillCritDamageAdd != 0)
                {
                    script += genHashSingle("Def_Skill_CritDamage_Add", equipment.DefSkillCritDamageAdd);
                    position++;
                    index++;
                }
            }

            // Defense against skill special critical rate
            if (equipment.CheckDefSkillSpCritRate())
            {
                if (equipment.DefSkillSpCritRateMul != 0)
                {
                    script += genHashSingle("Def_Skill_SpCritRate_Mul", equipment.DefSkillSpCritRateMul);
                    position++;
                    index++;
                }
                if (equipment.DefSkillSpCritRateAdd != 0)
                {
                    script += genHashSingle("Def_Skill_SpCritRate_Add", equipment.DefSkillSpCritRateAdd);
                    position++;
                    index++;
                }
            }

            // Defense against skill special critical damage
            if (equipment.CheckDefSkillSpCritDamage())
            {
                if (equipment.DefSkillSpCritDamageMul != 0)
                {
                    script += genHashSingle("Def_Skill_SpCritDamage_Mul", equipment.DefSkillSpCritDamageMul);
                    position++;
                    index++;
                }
                if (equipment.DefSkillSpCritDamageAdd != 0)
                {
                    script += genHashSingle("Def_Skill_SpCritDamage_Add", equipment.DefSkillSpCritDamageAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Defense

            // Physical defense
            if (equipment.CheckPDef())
            {
                if (equipment.PDefMul != 0)
                {
                    script += genHashSingle("PDef_Mul", equipment.PDefMul);
                    position++;
                    index++;
                }
                if (equipment.PDefAdd != 0)
                {
                    script += genHashSingle("PDef_Add", equipment.PDefAdd);
                    position++;
                    index++;
                }
            }

            // Magical defense
            if (equipment.CheckMDef())
            {
                if (equipment.MDefMul != 0)
                {
                    script += genHashSingle("MDef_Mul", equipment.MDefMul);
                    position++;
                    index++;
                }
                if (equipment.MDefAdd != 0)
                {
                    script += genHashSingle("MDef_Add", equipment.MDefAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            return script;
        }

        #endregion

        #region Armor

        private static string genDataPackArmor(DataPackEquipment equipment)
        {
            index = 0;
            position = 0;
            column = 3;

            string script = "";

            column = 3;

            #region Equipment

            // Type (Armor only)
            if (equipment.CheckType())
            {
                script += genHashSingle("Kind", equipment.Type);
                position++;
                index++;
            }

            // Cursed
            if (equipment.CheckCursed())
            {
                script += genHashSingle("Cursed", (equipment.Cursed.ToString()).ToLower());
                position++;
                index++;
            }

            // Switch ID
            if (equipment.CheckSwitchID())
            {
                script += genHashSingle("SwitchID", equipment.SwitchID.ToString());
                position++;
                index++;
            }

            #endregion

            #region Defense

            // Physical defense
            if (equipment.CustomPDef && equipment.PDefInitial != cfg.Default.PDefInitial)
            {
                script += genHashSingle("PDef", equipment.PDefInitial);
                position++;
                index++;
            }

            // Magical defense
            if (equipment.CustomMDef && equipment.MDefInitial != cfg.Default.MDefInitial)
            {
                script += genHashSingle("MDef", equipment.MDefInitial);
                position++;
                index++;
            }

            #endregion

            #region Parameter

            // Maximum HP
            if (equipment.CheckMaxHP())
            {
                if (equipment.MaxHPMul != 0)
                {
                    script += genHashSingle("MaxHP_Mul", equipment.MaxHPMul);
                    position++;
                    index++;
                }
                if (equipment.MaxHPAdd != 0)
                {
                    script += genHashSingle("MaxHP_Add", equipment.MaxHPAdd);
                    position++;
                    index++;
                }
            }

            // Maximum HP
            if (equipment.CheckMaxSP())
            {
                if (equipment.MaxSPMul != 0)
                {
                    script += genHashSingle("MaxSP_Mul", equipment.MaxSPMul);
                    position++;
                    index++;
                }
                if (equipment.MaxSPAdd != 0)
                {
                    script += genHashSingle("MaxSP_Add", equipment.MaxSPAdd);
                    position++;
                    index++;
                }
            }

            // Strengh
            if (equipment.CheckStr())
            {
                if (equipment.StrMul != 0)
                {
                    script += genHashSingle("Str_Mul", equipment.StrMul);
                    position++;
                    index++;
                }
                if (equipment.StrAdd != 0)
                {
                    script += genHashSingle("Str_Add", equipment.StrAdd);
                    position++;
                    index++;
                }
            }

            // Dexterity
            if (equipment.CheckDex())
            {
                if (equipment.DexMul != 0)
                {
                    script += genHashSingle("Dex_Mul", equipment.DexMul);
                    position++;
                    index++;
                }
                if (equipment.DexAdd != 0)
                {
                    script += genHashSingle("Dex_Add", equipment.DexAdd);
                    position++;
                    index++;
                }
            }

            // Agility
            if (equipment.CheckAgi())
            {
                if (equipment.AgiMul != 0)
                {
                    script += genHashSingle("Agi_Mul", equipment.AgiMul);
                    position++;
                    index++;
                }
                if (equipment.AgiAdd != 0)
                {
                    script += genHashSingle("Agi_Add", equipment.AgiAdd);
                    position++;
                    index++;
                }
            }

            // Intelligence
            if (equipment.CheckInt())
            {
                if (equipment.IntMul != 0)
                {
                    script += genHashSingle("Int_Mul", equipment.IntMul);
                    position++;
                    index++;
                }
                if (equipment.IntAdd != 0)
                {
                    script += genHashSingle("Int_Add", equipment.IntAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Parameter Rate

            // Intelligence
            if (equipment.CheckStrRate())
            {
                if (equipment.StrRateMul != 0)
                {
                    script += genHashSingle("StrRate_Mul", equipment.StrRateMul);
                    position++;
                    index++;
                }
                if (equipment.StrRateAdd != 0)
                {
                    script += genHashSingle("StrRate_Mul", equipment.StrRateAdd);
                    position++;
                    index++;
                }
            }

            // Dexterity attack rate
            if (equipment.CheckDexRate())
            {
                if (equipment.DexRateMul != 0)
                {
                    script += genHashSingle("DexRate_Mul", equipment.DexRateMul);
                    position++;
                    index++;
                }
                if (equipment.DexRateAdd != 0)
                {
                    script += genHashSingle("DexRate_Add", equipment.DexRateAdd);
                    position++;
                    index++;
                }
            }

            // Agility attack rate
            if (equipment.CheckAgiRate())
            {
                if (equipment.AgiRateMul != 0)
                {
                    script += genHashSingle("AgiRate_Mul", equipment.AgiRateMul);
                    position++;
                    index++;
                }
                if (equipment.AgiRateAdd != 0)
                {
                    script += genHashSingle("AgiRate_Mul", equipment.AgiRateAdd);
                    position++;
                    index++;
                }
            }

            // Intelligence attack rate
            if (equipment.CheckIntRate())
            {
                if (equipment.IntRateMul != 0)
                {
                    script += genHashSingle("IntRate_Mul", equipment.IntRateMul);
                    position++;
                    index++;
                }
                if (equipment.IntRateAdd != 0)
                {
                    script += genHashSingle("IntRate_Add", equipment.IntRateAdd);
                    position++;
                    index++;
                }
            }

            // Physical defense attack rate
            if (equipment.CheckPDefRate())
            {
                if (equipment.PDefRateMul != 0)
                {
                    script += genHashSingle("PDefRate_Mul", equipment.PDefRateMul);
                    position++;
                    index++;
                }
                if (equipment.PDefRateAdd != 0)
                {
                    script += genHashSingle("PDefRate_Add", equipment.PDefRateAdd);
                    position++;
                    index++;
                }
            }

            // Magical defense attack rate
            if (equipment.CheckMDefRate())
            {
                if (equipment.MDefRateMul != 0)
                {
                    script += genHashSingle("MDefRate_Mul", equipment.MDefRateMul);
                    position++;
                    index++;
                }
                if (equipment.MDefRateAdd != 0)
                {
                    script += genHashSingle("MDefRate_Add", equipment.MDefRateAdd);
                    position++;
                    index++;
                }
            }

            // Guard rate
            if (equipment.CheckGuardRate())
            {
                if (equipment.GuardRateMul != 0)
                {
                    script += genHashSingle("GuardRate_Mul", equipment.GuardRateMul);
                    position++;
                    index++;
                }
                if (equipment.GuardRateAdd != 0)
                {
                    script += genHashSingle("GuardRate_Add", equipment.GuardRateAdd);
                    position++;
                    index++;
                }
            }

            // Evasion rate
            if (equipment.CheckEvaRate())
            {
                if (equipment.EvaRateMul != 0)
                {
                    script += genHashSingle("EvaRate_Mul", equipment.EvaRateMul);
                    position++;
                    index++;
                }
                if (equipment.EvaRateAdd != 0)
                {
                    script += genHashSingle("EvaRate_Add", equipment.EvaRateAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Defense against Attack Critical

            // Defense against attack critical rate
            if (equipment.CheckDefCritRate())
            {
                if (equipment.DefCritRateMul != 0)
                {
                    script += genHashSingle("Def_CritRate_Mul", equipment.DefCritRateMul);
                    position++;
                    index++;
                }
                if (equipment.DefCritRateAdd != 0)
                {
                    script += genHashSingle("Def_CritRate_Add", equipment.DefCritRateAdd);
                    position++;
                    index++;
                }
            }

            // Defense against attack critical damage
            if (equipment.CheckDefCritDamage())
            {
                if (equipment.DefCritDamageMul != 0)
                {
                    script += genHashSingle("Def_CritDamage_Mul", equipment.DefCritDamageMul);
                    position++;
                    index++;
                }
                if (equipment.DefCritDamageAdd != 0)
                {
                    script += genHashSingle("Def_CritDamage_Add", equipment.DefCritDamageAdd);
                    position++;
                    index++;
                }
            }

            // Defense against attack special critical rate
            if (equipment.CheckDefSpCritRate())
            {
                if (equipment.DefSpCritRateMul != 0)
                {
                    script += genHashSingle("Def_SpCritRate_Mul", equipment.DefSpCritRateMul);
                    position++;
                    index++;
                }
                if (equipment.DefSpCritRateAdd != 0)
                {
                    script += genHashSingle("Def_SpCritRate_Add", equipment.DefSpCritRateAdd);
                    position++;
                    index++;
                }
            }

            // Strengh
            if (equipment.CheckDefSpCritDamage())
            {
                if (equipment.DefSpCritDamageMul != 0)
                {
                    script += genHashSingle("Def_SpCritDamage_Mul", equipment.DefSpCritDamageMul);
                    position++;
                    index++;
                }
                if (equipment.DefSpCritDamageAdd != 0)
                {
                    script += genHashSingle("Def_SpCritDamage_Add", equipment.DefSpCritDamageAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Defense against Skill Critical

            // Defense against skill critical rate
            if (equipment.CheckDefSkillCritRate())
            {
                if (equipment.DefSkillCritRateMul != 0)
                {
                    script += genHashSingle("Def_Skill_CritRate_Mul", equipment.DefSkillCritRateMul);
                    position++;
                    index++;
                }
                if (equipment.DefSkillCritRateAdd != 0)
                {
                    script += genHashSingle("Def_Skill_CritRate_Add", equipment.DefSkillCritRateAdd);
                    position++;
                    index++;
                }
            }

            // Defense against skill critical damage
            if (equipment.CheckDefSkillCritDamage())
            {
                if (equipment.DefSkillCritDamageMul != 0)
                {
                    script += genHashSingle("Def_Skill_CritDamage_Mul", equipment.DefSkillCritDamageMul);
                    position++;
                    index++;
                }
                if (equipment.DefSkillCritDamageAdd != 0)
                {
                    script += genHashSingle("Def_Skill_CritDamage_Add", equipment.DefSkillCritDamageAdd);
                    position++;
                    index++;
                }
            }

            // Defense against skill special critical rate
            if (equipment.CheckDefSkillSpCritRate())
            {
                if (equipment.DefSkillSpCritRateMul != 0)
                {
                    script += genHashSingle("Def_Skill_SpCritRate_Mul", equipment.DefSkillSpCritRateMul);
                    position++;
                    index++;
                }
                if (equipment.DefSkillSpCritRateAdd != 0)
                {
                    script += genHashSingle("Def_Skill_SpCritRate_Add", equipment.DefSkillSpCritRateAdd);
                    position++;
                    index++;
                }
            }

            // Defense against skill special critical damage
            if (equipment.CheckDefSkillSpCritDamage())
            {
                if (equipment.DefSkillSpCritDamageMul != 0)
                {
                    script += genHashSingle("Def_Skill_SpCritDamage_Mul", equipment.DefSkillSpCritDamageMul);
                    position++;
                    index++;
                }
                if (equipment.DefSkillSpCritDamageAdd != 0)
                {
                    script += genHashSingle("Def_Skill_SpCritDamage_Add", equipment.DefSkillSpCritDamageAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Attack

            // Attack
            if (equipment.CheckAtk())
            {
                if (equipment.AtkMul != 0)
                {
                    script += genHashSingle("Atk_Mul", equipment.AtkMul);
                    position++;
                    index++;
                }
                if (equipment.AtkAdd != 0)
                {
                    script += genHashSingle("Atk_Add", equipment.AtkAdd);
                    position++;
                    index++;
                }
            }

            // Hit rate
            if (equipment.CheckHit())
            {
                if (equipment.HitMul != 0)
                {
                    script += genHashSingle("HitRate_Mul", equipment.HitMul);
                    position++;
                    index++;
                }
                if (equipment.HitAdd != 0)
                {
                    script += genHashSingle("HitRate_Add", equipment.HitAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Critical

            // Critical rate
            if (equipment.CheckCritRate())
            {
                if (equipment.CritRateMul != 0)
                {
                    script += genHashSingle("CritRate_Mul", equipment.CritRateMul);
                    position++;
                    index++;
                }
                if (equipment.CritRateAdd != 0)
                {
                    script += genHashSingle("CritRate_Add", equipment.CritRateAdd);
                    position++;
                    index++;
                }
            }

            // Critical damage
            if (equipment.CheckCritDamage())
            {
                if (equipment.CritDamageMul != 0)
                {
                    script += genHashSingle("CritDamage_Mul", equipment.CritDamageMul);
                    position++;
                    index++;
                }
                if (equipment.CritDamageAdd != 0)
                {
                    script += genHashSingle("CritDamage_Add", equipment.CritDamageAdd);
                    position++;
                    index++;
                }
            }

            // Critical guard rate reduction
            if (equipment.CheckCritDefGuard())
            {
                if (equipment.CritDefGuardMul != 0)
                {
                    script += genHashSingle("Crit_DefGuard_Mul", equipment.CritDefGuardMul);
                    position++;
                    index++;
                }
                if (equipment.CritDefGuardAdd != 0)
                {
                    script += genHashSingle("Crit_DefGuard_Add", equipment.CritDefGuardAdd);
                    position++;
                    index++;
                }
            }

            // Critical evasion rate reduction
            if (equipment.CheckCritDefEva())
            {
                if (equipment.CritDefEvaMul != 0)
                {
                    script += genHashSingle("Crit_DefEva_Mul", equipment.CritDefEvaMul);
                    position++;
                    index++;
                }
                if (equipment.CritDefEvaAdd != 0)
                {
                    script += genHashSingle("Crit_DefEva_Add", equipment.CritDefEvaAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Special Critical

            // Special critical rate
            if (equipment.CheckSpCritRate())
            {
                if (equipment.SpCritRateMul != 0)
                {
                    script += genHashSingle("SpCritRate_Mul", equipment.SpCritRateMul);
                    position++;
                    index++;
                }
                if (equipment.SpCritRateAdd != 0)
                {
                    script += genHashSingle("SpCritRate_Add", equipment.SpCritRateAdd);
                    position++;
                    index++;
                }
            }

            // Special critical damage
            if (equipment.CheckSpCritDamage())
            {
                if (equipment.SpCritDamageMul != 0)
                {
                    script += genHashSingle("SpCritDamage_Mul", equipment.SpCritDamageMul);
                    position++;
                    index++;
                }
                if (equipment.SpCritDamageAdd != 0)
                {
                    script += genHashSingle("SpCritDamage_Add", equipment.SpCritDamageAdd);
                    position++;
                    index++;
                }
            }

            // Special critical guard rate reduction
            if (equipment.CheckSpCritDefGuard())
            {
                if (equipment.SpCritDefGuardMul != 0)
                {
                    script += genHashSingle("SpCrit_DefGuard_Mul", equipment.SpCritDefGuardMul);
                    position++;
                    index++;
                }
                if (equipment.SpCritDefGuardAdd != 0)
                {
                    script += genHashSingle("SpCrit_DefGuard_Add", equipment.SpCritDefGuardAdd);
                    position++;
                    index++;
                }
            }

            // Special critical evasion rate reduction
            if (equipment.CheckSpCritDefEva())
            {
                if (equipment.SpCritDefEvaMul != 0)
                {
                    script += genHashSingle("SpCrit_DefEva_Mul", equipment.SpCritDefEvaMul);
                    position++;
                    index++;
                }
                if (equipment.SpCritDefEvaAdd != 0)
                {
                    script += genHashSingle("SpCrit_DefEva_Add", equipment.SpCritDefEvaAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            return script;
        }

        #endregion

        #region Enemy

        private static string genDataPackEnemy(DataPackEnemy enemy)
        {
            index = 0;
            position = 0;
            column = 3;

            string script = "";

            #region Parameter

            // Maximum HP
            if (enemy.CustomMaxHP && enemy.MaxHPInitial != cfg.Default.MaxHPInitial)
            {
                script += genHashSingle("MaxHP", enemy.MaxHPInitial);
                position++;
                index++;
            }

            // Maximum SP
            if (enemy.CustomMaxSP && enemy.MaxSPInitial != cfg.Default.MaxSPInitial)
            {
                script += genHashSingle("MaxSP", enemy.MaxSPInitial);
                position++;
                index++;
            }

            // Strengh
            if (enemy.CustomStr && enemy.StrInitial != cfg.Default.StrInitial)
            {
                script += genHashSingle("Str", enemy.StrInitial);
                position++;
                index++;
            }

            // Dexterity
            if (enemy.CustomDex && enemy.DexInitial != cfg.Default.DexInitial)
            {
                script += genHashSingle("Dex", enemy.DexInitial);
                position++;
                index++;
            }

            // Agility
            if (enemy.CustomAgi && enemy.AgiInitial != cfg.Default.AgiInitial)
            {
                script += genHashSingle("Agi", enemy.AgiInitial);
                position++;
                index++;
            }

            // Intelligence
            if (enemy.CustomInt && enemy.IntInitial != cfg.Default.IntInitial)
            {
                script += genHashSingle("Int", enemy.IntInitial);
                position++;
                index++;
            }

            #endregion

            #region Parameter Rate

            // Strengh attack rate
            if (enemy.CustomStrRate && enemy.StrRate != cfg.Default.StrRate)
            {
                script += genHashSingle("StrRate", enemy.StrRate);
                position++;
                index++;
            }

            // Dexterity attack rate
            if (enemy.CustomDexRate && enemy.DexRate != cfg.Default.DexRate)
            {
                script += genHashSingle("DexRate", enemy.DexRate);
                position++;
                index++;
            }

            // Agility attack rate
            if (enemy.CustomAgiRate && enemy.AgiRate != cfg.Default.AgiRate)
            {
                script += genHashSingle("AgiRate", enemy.AgiRate);
                position++;
                index++;
            }

            // Intelligence attack rate
            if (enemy.CustomIntRate && enemy.IntRate != cfg.Default.IntRate)
            {
                script += genHashSingle("IntRate", enemy.IntRate);
                position++;
                index++;
            }

            // Physical defense attack rate
            if (enemy.CustomPDefRate && enemy.PDefRate != cfg.Default.PDefRate)
            {
                script += genHashSingle("PDefRate", enemy.PDefRate);
                position++;
                index++;
            }

            // Magical defense attack rate
            if (enemy.CustomMDefRate && enemy.MDefRate != cfg.Default.MDefRate)
            {
                script += genHashSingle("MDefRate", enemy.MDefRate);
                position++;
                index++;
            }

            // Guard rate
            if (enemy.CustomGuardRate && enemy.GuardRate != cfg.Default.GuardRate)
            {
                script += genHashSingle("GuardRate", enemy.GuardRate);
                position++;
                index++;
            }

            // Evasion rate
            if (enemy.CustomEvaRate && enemy.EvaRate != cfg.Default.EvaRate)
            {
                script += genHashSingle("EvaRate", enemy.EvaRate * 100);
                position++;
                index++;
            }

            #endregion

            #region Defense against Attack Critical

            // Defense against attack critical rate
            if (enemy.CustomDefCritRate && enemy.DefCritRate != cfg.Default.DefCritRate)
            {
                script += genHashSingle("Def_CritRate", enemy.DefCritRate);
                position++;
                index++;
            }

            // Defense against attack critical damage
            if (enemy.CustomDefCritDamage && enemy.DefCritDamage != cfg.Default.DefCritDamage)
            {
                script += genHashSingle("Def_CritDamage", enemy.DefCritDamage);
                position++;
                index++;
            }

            // Defense against attack special critical rate
            if (enemy.CustomDefSpCritRate && enemy.DefSpCritRate != cfg.Default.DefSpCritRate)
            {
                script += genHashSingle("Def_SpCritRate", enemy.DefSpCritRate);
                position++;
                index++;
            }

            // Defense against attack special critical damage
            if (enemy.CustomDefSpCritDamage && enemy.DefSpCritDamage != cfg.Default.DefSpCritDamage)
            {
                script += genHashSingle("Def_SpCritDamage", enemy.DefSpCritDamage);
                position++;
                index++;
            }

            #endregion

            #region Defense against Skill Critical

            // Defense against skill critical rate
            if (enemy.CustomDefSkillCritRate && enemy.DefSkillCritRate != cfg.Default.DefSkillCritRate)
            {
                script += genHashSingle("Def_Skill_CritRate", enemy.DefSkillCritRate);
                position++;
                index++;
            }

            // Defense against skill critical damage
            if (enemy.CustomDefSkillCritDamage && enemy.DefSkillCritDamage != cfg.Default.DefSkillCritDamage)
            {
                script += genHashSingle("Def_Skill_CritDamage", enemy.DefSkillCritDamage);
                position++;
                index++;
            }

            // Defense against skill special critical rate
            if (enemy.CustomDefSkillSpCritRate && enemy.DefSkillSpCritRate != cfg.Default.DefSkillSpCritRate)
            {
                script += genHashSingle("Def_Skill_SpCritRate", enemy.DefSkillSpCritRate);
                position++;
                index++;
            }

            // Defense against skill special critical damage
            if (enemy.CustomDefSkillSpCritDamage && enemy.DefSkillSpCritDamage != cfg.Default.DefSkillSpCritDamage)
            {
                script += genHashSingle("Def_Skill_SpCritDamage", enemy.DefSkillSpCritDamage);
                position++;
                index++;
            }

            #endregion

            #region Attack

            // Attack
            if (enemy.CustomAtk && enemy.AtkInitial != cfg.Default.AtkInitial)
            {
                script += genHashSingle("Atk", enemy.AtkInitial);
                position++;
                index++;
            }

            // Hit rate
            if (enemy.CustomHit && enemy.HitInitial != cfg.Default.HitInitial)
            {
                script += genHashSingle("HitRate", enemy.HitInitial * 100);
                position++;
                index++;
            }

            // Strengh attack force
            if (enemy.CustomParamForce)
            {
                if (enemy.StrForce != cfg.Default.StrForce)
                {
                    script += genHashSingle("Str_Force", enemy.StrForce);
                    position++;
                    index++;
                }

                // Dexterity attack force
                if (enemy.DexForce != cfg.Default.DexForce)
                {
                    script += genHashSingle("Dex_Force", enemy.DexForce);
                    position++;
                    index++;
                }

                // Agility attack force
                if (enemy.AgiForce != cfg.Default.AgiForce)
                {
                    script += genHashSingle("Agi_Force", enemy.AgiForce);
                    position++;
                    index++;
                }

                // Intelligence attack force
                if (enemy.IntForce != cfg.Default.IntForce)
                {
                    script += genHashSingle("Int_Force", enemy.IntForce);
                    position++;
                    index++;
                }
            }

            // Physical defense attack force
            if (enemy.CustomDefenseForce)
            {
                if (enemy.PDefForce != cfg.Default.PDefForce)
                {
                    script += genHashSingle("PDef_Force", enemy.PDefForce);
                    position++;
                    index++;
                }

                // Magical defense attack force
                if (enemy.MDefForce != cfg.Default.MDefForce)
                {
                    script += genHashSingle("MDef_Force", enemy.MDefForce);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Critical

            // Critical rate
            if (enemy.CustomCritRate && enemy.CritRate != cfg.Default.CritRate)
            {
                script += genHashSingle("CritRate", enemy.CritRate * 100);
                position++;
                index++;
            }

            // Critical damage
            if (enemy.CustomCritDamage && enemy.CritDamage != cfg.Default.CritDamage)
            {
                script += genHashSingle("CritDamage", enemy.CritDamage);
                position++;
                index++;
            }

            // Critical guard rate reduction
            if (enemy.CustomCritDefGuard && enemy.CritDefGuard != cfg.Default.CritDefGuard)
            {
                script += genHashSingle("Crit_DefGuard", enemy.CritDefGuard);
                position++;
                index++;
            }

            // Critical evasion rate reduction
            if (enemy.CustomCritDefEva && enemy.CritDefEva != cfg.Default.CritDefEva)
            {
                script += genHashSingle("Crit_DefEva", enemy.CritDefEva);
                position++;
                index++;
            }

            #endregion

            #region Special Critical

            // Special critical rate
            if (enemy.CustomSpCritRate && enemy.SpCritRate != cfg.Default.SpCritRate)
            {
                script += genHashSingle("SpCritRate", enemy.SpCritRate * 100);
                position++;
                index++;
            }

            // Special critical damage
            if (enemy.CustomSpCritDamage && enemy.SpCritDamage != cfg.Default.SpCritDamage)
            {
                script += genHashSingle("SpCritDamage", enemy.SpCritDamage);
                position++;
                index++;
            }

            // Special critical guard rate reduction
            if (enemy.CustomSpCritDefGuard && enemy.SpCritDefGuard != cfg.Default.SpCritDefGuard)
            {
                script += genHashSingle("SpCrit_DefGuard", enemy.SpCritDefGuard);
                position++;
                index++;
            }

            // Special critical evasion rate reduction
            if (enemy.CustomSpCritDefEva && enemy.SpCritDefEva != cfg.Default.SpCritDefEva)
            {
                script += genHashSingle("SpCrit_DefEva", enemy.SpCritDefEva);
                position++;
                index++;
            }

            #endregion

            #region Defense

            // Physical defense
            if (enemy.CustomPDef && enemy.PDefInitial != cfg.Default.PDefInitial)
            {
                script += genHashSingle("PDef", enemy.PDefInitial);
                position++;
                index++;
            }

            // Magical defense
            if (enemy.CustomMDef && enemy.MDefInitial != cfg.Default.MDefInitial)
            {
                script += genHashSingle("MDef", enemy.MDefInitial);
                position++;
                index++;
            }

            #endregion

            return script;
        }

        #endregion

        #region State

        private static string genDataPackState(DataPackState state)
        {
            index = 0;
            position = 0;
            string script = "";

            column = 3;

            #region Parameter

            // Maximum HP
            if (state.CheckMaxHP())
            {
                if (state.MaxHPMul != 0)
                {
                    script += genHashSingle("MaxHP_Mul", state.MaxHPMul);
                    position++;
                    index++;
                }
                if (state.MaxHPAdd != 0)
                {
                    script += genHashSingle("MaxHP_Add", state.MaxHPAdd);
                    position++;
                    index++;
                }
            }

            // Maximum HP
            if (state.CheckMaxSP())
            {
                if (state.MaxSPMul != 0)
                {
                    script += genHashSingle("MaxSP_Mul", state.MaxSPMul);
                    position++;
                    index++;
                }
                if (state.MaxSPAdd != 0)
                {
                    script += genHashSingle("MaxSP_Add", state.MaxSPAdd);
                    position++;
                    index++;
                }
            }

            // Strengh
            if (state.CheckStr())
            {
                if (state.StrMul != 0)
                {
                    script += genHashSingle("Str_Mul", state.StrMul);
                    position++;
                    index++;
                }
                if (state.StrAdd != 0)
                {
                    script += genHashSingle("Str_Add", state.StrAdd);
                    position++;
                    index++;
                }
            }

            // Dexterity
            if (state.CheckDex())
            {
                if (state.DexMul != 0)
                {
                    script += genHashSingle("Dex_Mul", state.DexMul);
                    position++;
                    index++;
                }
                if (state.DexAdd != 0)
                {
                    script += genHashSingle("Dex_Add", state.DexAdd);
                    position++;
                    index++;
                }
            }

            // Agility
            if (state.CheckAgi())
            {
                if (state.AgiMul != 0)
                {
                    script += genHashSingle("Agi_Mul", state.AgiMul);
                    position++;
                    index++;
                }
                if (state.AgiAdd != 0)
                {
                    script += genHashSingle("Agi_Add", state.AgiAdd);
                    position++;
                    index++;
                }
            }

            // Intelligence
            if (state.CheckInt())
            {
                if (state.IntMul != 0)
                {
                    script += genHashSingle("Int_Mul", state.IntMul);
                    position++;
                    index++;
                }
                if (state.IntAdd != 0)
                {
                    script += genHashSingle("Int_Add", state.IntAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Parameter Rate

            // Intelligence
            if (state.CheckStrRate())
            {
                if (state.StrRateMul != 0)
                {
                    script += genHashSingle("StrRate_Mul", state.StrRateMul);
                    position++;
                    index++;
                }
                if (state.StrRateAdd != 0)
                {
                    script += genHashSingle("StrRate_Add", state.StrRateAdd);
                    position++;
                    index++;
                }
            }

            // Dexterity attack rate
            if (state.CheckDexRate())
            {
                if (state.DexRateMul != 0)
                {
                    script += genHashSingle("DexRate_Mul", state.DexRateMul);
                    position++;
                    index++;
                }
                if (state.DexRateAdd != 0)
                {
                    script += genHashSingle("DexRate_Add", state.DexRateAdd);
                    position++;
                    index++;
                }
            }

            // Agility attack rate
            if (state.CheckAgiRate())
            {
                if (state.AgiRateMul != 0)
                {
                    script += genHashSingle("AgiRate_Mul", state.AgiRateMul);
                    position++;
                    index++;
                }
                if (state.AgiRateAdd != 0)
                {
                    script += genHashSingle("AgiRate_Add", state.AgiRateAdd);
                    position++;
                    index++;
                }
            }

            // Intelligence attack rate
            if (state.CheckIntRate())
            {
                if (state.IntRateMul != 0)
                {
                    script += genHashSingle("IntRate_Mul", state.IntRateMul);
                    position++;
                    index++;
                }
                if (state.IntRateAdd != 0)
                {
                    script += genHashSingle("IntRate_Add", state.IntRateAdd);
                    position++;
                    index++;
                }
            }

            // Physical defense attack rate
            if (state.CheckPDefRate())
            {
                if (state.PDefRateMul != 0)
                {
                    script += genHashSingle("PDefRate_Mul", state.PDefRateMul);
                    position++;
                    index++;
                }
                if (state.PDefRateAdd != 0)
                {
                    script += genHashSingle("PDefRate_Add", state.PDefRateAdd);
                    position++;
                    index++;
                }
            }

            // Magical defense attack rate
            if (state.CheckMDefRate())
            {
                if (state.MDefRateMul != 0)
                {
                    script += genHashSingle("MDefRate_Mul", state.MDefRateMul);
                    position++;
                    index++;
                }
                if (state.MDefRateAdd != 0)
                {
                    script += genHashSingle("MDefRate_Add", state.MDefRateAdd);
                    position++;
                    index++;
                }
            }

            // Guard rate
            if (state.CheckGuardRate())
            {
                if (state.GuardRateMul != 0)
                {
                    script += genHashSingle("GuardRate_Mul", state.GuardRateMul);
                    position++;
                    index++;
                }
                if (state.GuardRateAdd != 0)
                {
                    script += genHashSingle("GuardRate_Add", state.GuardRateAdd);
                    position++;
                    index++;
                }
            }

            // Evasion rate
            if (state.CheckEvaRate())
            {
                if (state.EvaRateMul != 0)
                {
                    script += genHashSingle("EvaRate_Mul", state.EvaRateMul);
                    position++;
                    index++;
                }
                if (state.EvaRateAdd != 0)
                {
                    script += genHashSingle("EvaRate_Add", state.EvaRateAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Defense against Attack Critical

            // Defense against attack critical rate
            if (state.CheckDefCritRate())
            {
                if (state.DefCritRateMul != 0)
                {
                    script += genHashSingle("Def_CritRate_Mul", state.DefCritRateMul);
                    position++;
                    index++;
                }
                if (state.DefCritRateAdd != 0)
                {
                    script += genHashSingle("Def_CritRate_Add", state.DefCritRateAdd);
                    position++;
                    index++;
                }
            }

            // Defense against attack critical damage
            if (state.CheckDefCritDamage())
            {
                if (state.DefCritDamageMul != 0)
                {
                    script += genHashSingle("Def_CritDamage_Mul", state.DefCritDamageMul);
                    position++;
                    index++;
                }
                if (state.DefCritDamageAdd != 0)
                {
                    script += genHashSingle("Def_CritDamage_Add", state.DefCritDamageAdd);
                    position++;
                    index++;
                }
            }

            // Defense against attack special critical rate
            if (state.CheckDefSpCritRate())
            {
                if (state.DefSpCritRateMul != 0)
                {
                    script += genHashSingle("Def_SpCritRate_Mul", state.DefSpCritRateMul);
                    position++;
                    index++;
                }
                if (state.DefSpCritRateAdd != 0)
                {
                    script += genHashSingle("Def_SpCritRate_Add", state.DefSpCritRateAdd);
                    position++;
                    index++;
                }
            }

            // Strengh
            if (state.CheckDefSpCritDamage())
            {
                if (state.DefSpCritDamageMul != 0)
                {
                    script += genHashSingle("Def_SpCritDamage_Mul", state.DefSpCritDamageMul);
                    position++;
                    index++;
                }
                if (state.DefSpCritDamageAdd != 0)
                {
                    script += genHashSingle("Def_SpCritDamage_Add", state.DefSpCritDamageAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Defense against Skill Critical

            // Defense against skill critical rate
            if (state.CheckDefSkillCritRate())
            {
                if (state.DefSkillCritRateMul != 0)
                {
                    script += genHashSingle("Def_Skill_CritRate_Mul", state.DefSkillCritRateMul);
                    position++;
                    index++;
                }
                if (state.DefSkillCritRateAdd != 0)
                {
                    script += genHashSingle("Def_Skill_CritRate_Add", state.DefSkillCritRateAdd);
                    position++;
                    index++;
                }
            }

            // Defense against skill critical damage
            if (state.CheckDefSkillCritDamage())
            {
                if (state.DefSkillCritDamageMul != 0)
                {
                    script += genHashSingle("Def_Skill_CritDamage_Mul", state.DefSkillCritDamageMul);
                    position++;
                    index++;
                }
                if (state.DefSkillCritDamageAdd != 0)
                {
                    script += genHashSingle("Def_Skill_CritDamage_Add", state.DefSkillCritDamageAdd);
                    position++;
                    index++;
                }
            }

            // Defense against skill special critical rate
            if (state.CheckDefSkillSpCritRate())
            {
                if (state.DefSkillSpCritRateMul != 0)
                {
                    script += genHashSingle("Def_Skill_SpCritRate_Mul", state.DefSkillSpCritRateMul);
                    position++;
                    index++;
                }
                if (state.DefSkillSpCritRateAdd != 0)
                {
                    script += genHashSingle("Def_Skill_SpCritRate_Add", state.DefSkillSpCritRateAdd);
                    position++;
                    index++;
                }
            }

            // Defense against skill special critical damage
            if (state.CheckDefSkillSpCritDamage())
            {
                if (state.DefSkillSpCritDamageMul != 0)
                {
                    script += genHashSingle("Def_Skill_SpCritDamage_Mul", state.DefSkillSpCritDamageMul);
                    position++;
                    index++;
                }
                if (state.DefSkillSpCritDamageAdd != 0)
                {
                    script += genHashSingle("Def_Skill_SpCritDamage_Add", state.DefSkillSpCritDamageAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Attack

            // Attack
            if (state.CheckAtk())
            {
                if (state.AtkMul != 0)
                {
                    script += genHashSingle("Atk_Mul", state.AtkMul);
                    position++;
                    index++;
                }
                if (state.AtkAdd != 0)
                {
                    script += genHashSingle("Atk_Add", state.AtkAdd);
                    position++;
                    index++;
                }
            }

            // Hit rate
            if (state.CheckHit())
            {
                if (state.HitMul != 0)
                {
                    script += genHashSingle("HitRate_Mul", state.HitMul);
                    position++;
                    index++;
                }
                if (state.HitAdd != 0)
                {
                    script += genHashSingle("HitRate_Add", state.HitAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Critical

            // Critical rate
            if (state.CheckCritRate())
            {
                if (state.CritRateMul != 0)
                {
                    script += genHashSingle("CritRate_Mul", state.CritRateMul);
                    position++;
                    index++;
                }
                if (state.CritRateAdd != 0)
                {
                    script += genHashSingle("CritRate_Add", state.CritRateAdd);
                    position++;
                    index++;
                }
            }

            // Critical damage
            if (state.CheckCritDamage())
            {
                if (state.CritDamageMul != 0)
                {
                    script += genHashSingle("CritDamage_Mul", state.CritDamageMul);
                    position++;
                    index++;
                }
                if (state.CritDamageAdd != 0)
                {
                    script += genHashSingle("CritDamage_Add", state.CritDamageAdd);
                    position++;
                    index++;
                }
            }

            // Critical guard rate reduction
            if (state.CheckCritDefGuard())
            {
                if (state.CritDefGuardMul != 0)
                {
                    script += genHashSingle("Crit_DefGuard_Mul", state.CritDefGuardMul);
                    position++;
                    index++;
                }
                if (state.CritDefGuardAdd != 0)
                {
                    script += genHashSingle("Crit_DefGuard_Add", state.CritDefGuardAdd);
                    position++;
                    index++;
                }
            }

            // Critical evasion rate reduction
            if (state.CheckCritDefEva())
            {
                if (state.CritDefEvaMul != 0)
                {
                    script += genHashSingle("Crit_DefEva_Mul", state.CritDefEvaMul);
                    position++;
                    index++;
                }
                if (state.CritDefEvaAdd != 0)
                {
                    script += genHashSingle("Crit_DefEva_Add", state.CritDefEvaAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Special Critical

            // Special critical rate
            if (state.CheckSpCritRate())
            {
                if (state.SpCritRateMul != 0)
                {
                    script += genHashSingle("SpCritRate_Mul", state.SpCritRateMul);
                    position++;
                    index++;
                }
                if (state.SpCritRateAdd != 0)
                {
                    script += genHashSingle("SpCritRate_Add", state.SpCritRateAdd);
                    position++;
                    index++;
                }
            }

            // Special critical damage
            if (state.CheckSpCritDamage())
            {
                if (state.SpCritDamageMul != 0)
                {
                    script += genHashSingle("SpCritDamage_Mul", state.SpCritDamageMul);
                    position++;
                    index++;
                }
                if (state.SpCritDamageAdd != 0)
                {
                    script += genHashSingle("SpCritDamage_Add", state.SpCritDamageAdd);
                    position++;
                    index++;
                }
            }

            // Special critical guard rate reduction
            if (state.CheckSpCritDefGuard())
            {
                if (state.SpCritDefGuardMul != 0)
                {
                    script += genHashSingle("SpCrit_DefGuard_Mul", state.SpCritDefGuardMul);
                    position++;
                    index++;
                }
                if (state.SpCritDefGuardAdd != 0)
                {
                    script += genHashSingle("SpCrit_DefGuard_Add", state.SpCritDefGuardAdd);
                    position++;
                    index++;
                }
            }

            // Special critical evasion rate reduction
            if (state.CheckSpCritDefEva())
            {
                if (state.SpCritDefEvaMul != 0)
                {
                    script += genHashSingle("SpCrit_DefEva_Mul", state.SpCritDefEvaMul);
                    position++;
                    index++;
                }
                if (state.SpCritDefEvaAdd != 0)
                {
                    script += genHashSingle("SpCrit_DefEva_Add", state.SpCritDefEvaAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            #region Defense

            // Physical defense
            if (state.CheckPDef())
            {
                if (state.PDefMul != 0)
                {
                    script += genHashSingle("PDef_Mul", state.PDefMul);
                    position++;
                    index++;
                }
                if (state.PDefAdd != 0)
                {
                    script += genHashSingle("PDef_Add", state.PDefAdd);
                    position++;
                    index++;
                }
            }

            // Magical defense
            if (state.CheckMDef())
            {
                if (state.MDefMul != 0)
                {
                    script += genHashSingle("MDef_Mul", state.MDefMul);
                    position++;
                    index++;
                }
                if (state.MDefAdd != 0)
                {
                    script += genHashSingle("MDef_Add", state.MDefAdd);
                    position++;
                    index++;
                }
            }

            #endregion

            return script;
        }

        #endregion

        #endregion

        #region Generate Code

        private static string genScript()
        {
            string script = genScriptHeader();
            script += genModuleGeneral();
            script += genModuleActor();
            script += genModuleClass();
            script += genModuleSkill();
            script += genModulePassiveSkill();
            script += genModuleWeapon();
            script += genModuleArmor();
            script += genModuleEnemy();
            script += genModuleState();
            script += genModuleDefault();
            depth--;
            script += genNewLine("end") + genNewLine();
            return script;
        }

        #region Script

        private static string genScriptHeader()
        {
            string script = genSigns(":=");
            script += genNewLine("# Advanced Actor and Enemy Engine - Configuration");
            script += genNewLine("# Version: " + AAEEData.Version.Script);
            script += genNewLine("# Author: TimeKeeper");
            script += genNewLine("# Date: TBD (Beta)");
            script += genNewLine(genSigns("-"));
            script += genNewLine("# The configuration " + AAEEData.Version.Cfg + "was generated using the Advanced Actor and Enemy");
            script += genNewLine("# Engine - Configuration Tool " + AAEEData.Version.App + ".");
            script += genNewLine(genSigns("-"));
            script += genNewLine("# Credits");
            script += genNewLine(genSigns("-"));
            script += genNewLine("# - T-C from Code Project for the Xml serializer.");
            script += genNewLine("# - Blizzard from chaos project for the rgss marshal reader.");
            script += genNewLine(genSigns(":="));
            script += genNewLine();
            script += genNewLine("module AAEE");
            depth++;
            return script;
        }

        #endregion

        #region General

        private static string genModuleGeneral()
        {
            index = 0;
            string script = genNewLine();
            script += genModuleHeader("General", "general");
            script += genNewLine("# Get the general configuration.");

            #region Stat Limit Bypass

            script += genVarSingle("StatLimitBypass", cfg.General.SetLimitBypass);
            script += genVarSingle("HPSPMaxLimit", cfg.General.SetHPSPMaxLimit);
            script += genVarSingle("StatMaxLimit", cfg.General.SetStatMaxLimit);

            #endregion

            #region Stat Limit Bypass

            script += genVarSingle("SkillParamRateType", cfg.General.SkillParamRateType);
            script += genVarSingle("SkillDefenseRateType", cfg.General.SkillDefenseRateType);

            #endregion

            #region Actor class behavior

            script += genVarSingle("OrderEquipmentList", cfg.General.OrderEquipmentList);
            script += genVarSingle("OrderEquipmentMultiplier", cfg.General.OrderEquipmentMultiplier);
            script += genVarSingle("OrderEquipmentFlags", cfg.General.OrderEquipmentFlags);
            script += genVarSingle("OrderHandReduce", cfg.General.OrderHandReduce);
            script += genVarSingle("OrderUnarmedAttackForce", cfg.General.OrderUnarmedAttackForce);

            #endregion

            #region Cursed

            script += genNewLine("CursedColor = Color.new(" + cfg.General.CursedColorRed.ToString() + ", " + cfg.General.CursedColorGreen.ToString() + ", " + cfg.General.CursedColorBlue.ToString() + ", " + cfg.General.CursedColorAlpha.ToString() + ")");
            script += genVarSingle("ShowCursed", cfg.General.SetShowCursed);
            script += genVarSingle("BlockCursed", cfg.General.SetBlockCursed);

            #endregion

            depth--;
            script += genNewLine("end");
            return script;
        }

        #endregion

        #region Default

        private static string genModuleDefault()
        {
            bool scriptCheck = false;
            index = 0;
            string script = genNewLine();
            script += genModuleHeader("Default", "default");
            script += genNewLine("# Get the default stat.");
            script += genNewLine("Stat = {");
            depth++;

            #region Equipment

            // Write the equip type list
            if (cfg.Default.EquipType.Count > 0)
            {
                scriptCheck = false;
                if (cfg.Default.EquipType.Count > 5)
                {
                    scriptCheck = true;
                }
                else
                {
                    foreach (EquipType equip in cfg.Default.EquipType)
                    {
                        if (equip.Name != AAEEData.EquipTypeName[equip.ID])
                        {
                            scriptCheck = true;
                        }
                    }
                }
                if (scriptCheck)
                {
                    position = 0;
                    column = 4;
                    script += genNewLine(":EquipName => { ");
                    depth++;
                    foreach (EquipType equip in cfg.Default.EquipType)
                    {
                        if (equip.ID > 4)
                        {
                            if (equip.Name != "")
                            {
                                script += genHashNewLine(equip.ID.ToString() + " => " + "'" + equip.Name + "'");
                                position++;
                            }
                            else
                            {
                                script += genHashNewLine(equip.ID.ToString() + " => " + "'" + cfg.Default.EquipType[4].Name + "'");
                                position++;
                            }
                        }
                        else
                        {
                            if (equip.Name != AAEEData.EquipTypeName[equip.ID])
                            {
                                script += genHashNewLine(equip.ID.ToString() + " => '" + equip.Name + "'");
                                position++;
                            }
                        }
                    }
                    depth--;
                    script += genNewLine("}");
                    index++;
                }
            }

            // Write the equip id list
            if (cfg.Default.EquipList.Count > 0)
            {
                position = 1;
                column = 1;
                script += genHashNewLine(":EquipList => {");
                foreach (EquipType equip in cfg.Default.EquipList)
                {
                    if (position > 1)
                    {
                        script += ", ";
                    }
                    if (equip.Name != cfg.Default.EquipType[equip.ID].Name)
                    {
                        script += equip.ID.ToString() + " => '" + equip.Name + "'";
                    }
                    else
                    {
                        script += equip.ID.ToString() + " => nil";
                    }
                    position++;
                }
                script += "}";
                index++;
            }
            position = 0;
            column = 3;

            // Dual Hold
            script += genHashSingle("DualHold", (cfg.Default.DualHold.ToString()).ToLower());
            position++;
            index++;

            // Dual hold name
            script += genHashDouble("DualHoldName", "'" + cfg.Default.DualHoldNameWeapon + "'", "'" + cfg.Default.DualHoldNameShield + "'");
            position++;
            index++;

            // Dual hold multiplier
            script += genHashDouble("DualHold_Mul", cfg.Default.DualHoldMulWeapon, cfg.Default.DualHoldMulShield);
            position++;
            index++;

            // Shield bypass
            script += genHashSingle("OffHandBypass", (cfg.Default.ShieldBypass.ToString()).ToLower());
            position++;
            index++;

            // Shield bypass multiplier
            script += genHashSingle("OffHandAtkMul", cfg.Default.ShieldBypassMul);
            position++;
            index++;

            // Weapon bypass
            script += genHashSingle("MainHandBypass", (cfg.Default.ShieldBypass.ToString()).ToLower());
            position++;
            index++;

            // Weapon bypass multiplier
            script += genHashSingle("MainHandAtkMul", cfg.Default.WeaponBypassMul);
            position++;
            index++;

            // Reduce hand
            script += genHashSingle("HandNeedReduce", cfg.Default.ReduceHand);
            position++;
            index++;

            // Reduce hand multiplier
            script += genHashSingle("HandReduceMul", cfg.Default.ReduceHandMul);
            position++;
            index++;

            #endregion

            #region Parameter rate

            script += genHashSingle("StrRate", cfg.Default.StrRate);
            position++;
            index++;

            // Dexterity attack rate
            script += genHashSingle("DexRate", cfg.Default.DexRate);
            position++;
            index++;

            // Agility attack rate
            script += genHashSingle("AgiRate", cfg.Default.AgiRate);
            position++;
            index++;

            // Intelligence attack rate
            script += genHashSingle("IntRate", cfg.Default.IntRate);
            position++;
            index++;

            // Physical defense attack rate
            script += genHashSingle("PDefRate", cfg.Default.PDefRate);
            position++;
            index++;

            // Magical defense attack rate
            script += genHashSingle("MDefRate", cfg.Default.MDefRate);
            position++;
            index++;

            // Guard rate
            script += genHashSingle("GuardRate", cfg.Default.GuardRate);
            position++;
            index++;

            // Evasion rate
            script += genHashSingle("EvaRate", cfg.Default.EvaRate * 100);
            position++;
            index++;

            #endregion

            #region Defense against attack critical

            // Defense against attack critical rate
            script += genHashSingle("Def_CritRate", cfg.Default.DefCritRate);
            position++;
            index++;

            // Defense against attack critical damage
            script += genHashSingle("Def_CritDamage", cfg.Default.DefCritDamage);
            position++;
            index++;

            // Defense against attack special critical rate
            script += genHashSingle("Def_SpCritRate", cfg.Default.DefSpCritRate);
            position++;
            index++;

            // Defense against attack special critical damage
            script += genHashSingle("Def_SpCritDamage", cfg.Default.DefSpCritDamage);
            position++;
            index++;

            #endregion

            #region Defense against skill critical

            // Defense against skill critical rate
            script += genHashSingle("Def_Skill_CritRate", cfg.Default.DefSkillCritRate);
            position++;
            index++;

            // Defense against skill critical damage
            script += genHashSingle("Def_Skill_CritDamage", cfg.Default.DefSkillCritDamage);
            position++;
            index++;

            // Defense against skill special critical rate
            script += genHashSingle("Def_Skill_SpCritRate", cfg.Default.DefSkillSpCritRate);
            position++;
            index++;

            // Defense against skill special critical damage
            script += genHashSingle("Def_Skill_SpCritDamage", cfg.Default.DefSkillSpCritDamage);
            position++;
            index++;

            #endregion

            #region Attack

            // Attack
            script += genHashDouble("Atk", cfg.Default.AtkInitial, cfg.Default.AtkFinal);
            position++;
            index++;

            // Hit rate
            script += genHashDouble("HitRate", cfg.Default.HitInitial * 100, cfg.Default.HitFinal * 100);
            position++;
            index++;

            // Attack animation
            script += genHashDouble("Anim", cfg.Default.AnimCaster, cfg.Default.AnimTarget);
            position++;
            index++;

            // Strengh attack force
            script += genHashSingle("Str_Force", cfg.Default.StrForce);
            position++;
            index++;

            // Dexterity attack force
            script += genHashSingle("Dex_Force", cfg.Default.DexForce);
            position++;
            index++;

            // Agility attack force
            script += genHashSingle("Agi_Force", cfg.Default.AgiForce);
            position++;
            index++;

            // Intelligence attack force
            script += genHashSingle("Int_Force", cfg.Default.IntForce);
            position++;
            index++;

            // Physical defense attack force
            script += genHashSingle("PDef_Force", cfg.Default.PDefForce);
            position++;
            index++;

            // Magical defense attack force
            script += genHashSingle("MDef_Force", cfg.Default.MDefForce);
            position++;
            index++;

            #endregion

            #region Critical

            // Unarmed critical rate
            script += genHashSingle("CritRate", cfg.Default.CritRate * 100);
            position++;
            index++;

            // Unarmed critical damage
            script += genHashSingle("CritDamage", cfg.Default.CritDamage);
            position++;
            index++;

            // Unarmed critical guard rate reduction
            script += genHashSingle("Crit_DefGuard", cfg.Default.CritDefGuard);
            position++;
            index++;

            // Unarmed critical evasion rate reduction
            script += genHashSingle("Crit_DefEva", cfg.Default.CritDefEva);
            position++;
            index++;

            #endregion

            #region Special critical

            // Unarmed special critical rate
            script += genHashSingle("SpCritRate", cfg.Default.SpCritRate * 100);
            position++;
            index++;

            // Unarmed special critical damage
            script += genHashSingle("SpCritDamage", cfg.Default.SpCritDamage);
            position++;
            index++;

            // Unarmed special critical guard rate reduction
            script += genHashSingle("SpCrit_DefGuard", cfg.Default.SpCritDefGuard);
            position++;
            index++;

            // Unarmed special critical evasion rate reduction
            script += genHashSingle("SpCrit_DefEva", cfg.Default.SpCritDefEva);
            position++;
            index++;

            #endregion

            #region Defense

            // Unarmoured Physical Defense
            script += genHashDouble("PDef", cfg.Default.PDefInitial, cfg.Default.PDefFinal);
            position++;
            index++;

            // Unarmoured Magical Defense
            script += genHashDouble("MDef", cfg.Default.MDefInitial, cfg.Default.MDefFinal);
            position++;
            index++;

            #endregion

            depth--;
            script += genNewLine("}");
            depth--;
            script += genNewLine("end");
            return script;
        }

        #endregion

        #region Actor

        private static string genModuleActor()
        {
            string tempScript = "";
            int actorPosition = 0;
            string script = genNewLine();
            script += genModuleHeader("Actor", "actor family, id and default");
            script += genNewLine("# Get the actor in the actor family.");
            script += genNewLine("Family = {");
            depth++;
            if (cfg.ActorFamily.Count > 0)
            {
                actorPosition = 0;
                foreach (DataPackActor actorFamily in cfg.ActorFamily)
                {
                    if (actorFamily.ActorFamily.Count > 0)
                    {
                        if (actorPosition > 0)
                        {
                            script += ", ";
                        }
                        script += genNewLine(":" + genName(actorFamily.ID, actorFamily.Name) + " => [");
                        position = 0;
                        foreach (Actor actor in actorFamily.ActorFamily)
                        {
                            if (position > 0)
                            {
                                script += ", ";
                            }
                            script += actor.ID.ToString();
                            position++;
                        }
                        script += "]";
                        actorPosition++;
                    }
                }
            }
            depth--;
            script += genNewLine("}");
            script += genNewLine("# Get the stats of the actor family and actor id.");
            script += genNewLine("Stat = {");
            depth++;
            if (cfg.ActorFamily.Count > 0)
            {
                actorPosition = 0;
                foreach (DataPackActor actorFamily in cfg.ActorFamily)
                {
                    if (actorFamily.ActorFamily.Count > 0)
                    {
                        depth++;
                        tempScript = genDataPackActor(actorFamily);
                        depth--;
                        if (tempScript != "")
                        {
                            if (actorPosition > 0)
                            {
                                script += ", ";
                            }
                            script += genNewLine(":" + genName(actorFamily.ID, actorFamily.Name) + " => {");
                            script += tempScript;
                            script += genNewLine("}");
                            actorPosition++;
                        }
                    }
                }
            }
            if (cfg.ActorID.Count > 0)
            {
                actorPosition = 0;
                foreach (DataPackActor actorID in cfg.ActorID)
                {
                    depth++;
                    tempScript = genDataPackActor(actorID);
                    depth--;
                    if (tempScript != "")
                    {
                        if (actorPosition > 0)
                        {
                            script += ", ";
                        }
                        script += genNewLine((actorID.ID).ToString() + " => {");
                        script += tempScript;
                        script += genNewLine("}");
                        actorPosition++;
                    }
                }
            }
            depth--;
            script += genNewLine("}");
            script += genNewLine("# Get the default stats of the actor family and actor id.");
            script += genNewLine("Default = {");
            depth++;
            tempScript = genDataPackActor(cfg.ActorDefault);
            if (tempScript != "")
            {
                script += tempScript;
            }
            depth--;
            script += genNewLine("}");
            depth--;
            script += genNewLine("end");
            return script;
        }

        #endregion

        #region Class

        private static string genModuleClass()
        {
            string tempScript = "";
            int classPosition = 0;
            string script = genNewLine();
            script += genModuleHeader("Class", "class family, id and default");
            script += genNewLine("# Get the class in the class family.");
            script += genNewLine("Family = {");
            depth++;
            if (cfg.ClassFamily.Count > 0)
            {
                classPosition = 0;
                foreach (DataPackClass classFamily in cfg.ClassFamily)
                {
                    if (classFamily.ClassFamily.Count > 0)
                    {
                        if (classPosition > 0)
                        {
                            script += ", ";
                        }
                        script += genNewLine(":" + genName(classFamily.ID, classFamily.Name) + " => [");
                        position = 0;
                        foreach (Class classes in classFamily.ClassFamily)
                        {
                            if (position > 0)
                            {
                                script += ", ";
                            }
                            script += classes.ID.ToString();
                            position++;
                        }
                        script += "]";
                        classPosition++;
                    }
                }
            }
            depth--;
            script += genNewLine("}");
            script += genNewLine("# Get the stats of the class family and class id.");
            script += genNewLine("Stat = {");
            depth++;
            if (cfg.ClassFamily.Count > 0)
            {
                classPosition = 0;
                foreach (DataPackClass classFamily in cfg.ClassFamily)
                {
                    if (classFamily.ClassFamily.Count > 0)
                    {
                        depth++;
                        tempScript = genDataPackClass(classFamily);
                        depth--;
                        if (tempScript != "")
                        {
                            if (classPosition > 0)
                            {
                                script += ", ";
                            }
                            script += genNewLine(":" + genName(classFamily.ID, classFamily.Name) + " => {");
                            script += tempScript;
                            script += genNewLine("}");
                            classPosition++;
                        }
                    }
                }
            }
            if (cfg.ClassID.Count > 0)
            {
                classPosition = 0;
                foreach (DataPackClass classID in cfg.ClassID)
                {
                    depth++;
                    tempScript = genDataPackClass(classID);
                    depth--;
                    if (tempScript != "")
                    {
                        if (classPosition > 0)
                        {
                            script += ", ";
                        }
                        script += genNewLine((classID.ID).ToString() + " => {");
                        script += tempScript;
                        script += genNewLine("}");
                        classPosition++;
                    }
                }
            }
            depth--;
            script += genNewLine("}");
            script += genNewLine("# Get the default stats of the class family and class id.");
            script += genNewLine("Default = {");
            depth++;
            tempScript = genDataPackClass(cfg.ClassDefault);
            if (tempScript != "")
            {
                script += tempScript;
            }
            depth--;
            script += genNewLine("}");
            depth--;
            script += genNewLine("end");
            return script;
        }

        #endregion

        #region Skill

        private static string genModuleSkill()
        {
            string tempScript = "";
            int skillPosition = 0;
            string script = genNewLine();
            script += genModuleHeader("Skill", " skill family, id and default");
            script += genNewLine("# Get the skill in the skill family.");
            script += genNewLine("Family = {");
            depth++;
            if (cfg.SkillFamily.Count > 0)
            {
                skillPosition = 0;
                foreach (DataPackSkill skillFamily in cfg.SkillFamily)
                {
                    if (skillFamily.SkillFamily.Count > 0)
                    {
                        if (skillPosition > 0)
                        {
                            script += ", ";
                        }
                        script += genNewLine(":" + genName(skillFamily.ID, skillFamily.Name) + " => [");
                        position = 0;
                        foreach (Skill skill in skillFamily.SkillFamily)
                        {
                            if (position > 0)
                            {
                                script += ", ";
                            }
                            script += skill.ID.ToString();
                            position++;
                        }
                        script += "]";
                        skillPosition++;
                    }
                }
            }
            depth--;
            script += genNewLine("}");
            script += genNewLine("# Get the stats of the skill family and skill id.");
            script += genNewLine("Stat = {");
            depth++;
            if (cfg.SkillFamily.Count > 0)
            {
                skillPosition = 0;
                foreach (DataPackSkill skillFamily in cfg.SkillFamily)
                {
                    if (skillFamily.SkillFamily.Count > 0)
                    {
                        depth++;
                        tempScript = genDataPackSkill(skillFamily);
                        depth--;
                        if (tempScript != "")
                        {
                            if (skillPosition > 0)
                            {
                                script += ", ";
                            }
                            script += genNewLine(":" + genName(skillFamily.ID, skillFamily.Name) + " => {");
                            script += tempScript;
                            script += genNewLine("}");
                            skillPosition++;
                        }
                    }
                }
            }
            if (cfg.SkillID.Count > 0)
            {
                skillPosition = 0;
                foreach (DataPackSkill skillID in cfg.SkillID)
                {
                    depth++;
                    tempScript = genDataPackSkill(skillID);
                    depth--;
                    if (tempScript != "")
                    {
                        if (skillPosition > 0)
                        {
                            script += ", ";
                        }
                        script += genNewLine((skillID.ID).ToString() + " => {");
                        script += tempScript;
                        script += genNewLine("}");
                        skillPosition++;
                    }
                }
            }
            depth--;
            script += genNewLine("}");
            script += genNewLine("# Get the default stats of the skill family and passive skill id.");
            script += genNewLine("Default = {");
            depth++;
            tempScript = genDataPackSkill(cfg.SkillDefault);
            if (tempScript != "")
            {
                script += tempScript;
            }
            depth--;
            script += genNewLine("}");
            depth--;
            script += genNewLine("end");
            return script;
        }

        #endregion

        #region Passive Skill

        private static string genModulePassiveSkill()
        {
            string tempScript = "";
            int passiveSkillPosition = 0;
            string script = genNewLine();
            script += genModuleHeader("PassiveSkill", "passive skill family, id and default");
            script += genNewLine("# Get the passive skill in the passive skill family.");
            script += genNewLine("Family = {");
            depth++;
            if (cfg.PassiveSkillFamily.Count > 0)
            {
                passiveSkillPosition = 0;
                foreach (DataPackPassiveSkill passiveSkillFamily in cfg.PassiveSkillFamily)
                {
                    if (passiveSkillFamily.PassiveSkillFamily.Count > 0)
                    {
                        if (passiveSkillPosition > 0)
                        {
                            script += ", ";
                        }
                        script += genNewLine(":" + genName(passiveSkillFamily.ID, passiveSkillFamily.Name) + " => [");
                        position = 0;
                        foreach (Skill skill in passiveSkillFamily.PassiveSkillFamily)
                        {
                            if (position > 0)
                            {
                                script += ", ";
                            }
                            script += skill.ID.ToString();
                            position++;
                        }
                        script += "]";
                        passiveSkillPosition++;
                    }
                }
            }
            depth--;
            script += genNewLine("}");
            script += genNewLine("# Get the stats of the passive skill family and passive skill id.");
            script += genNewLine("Stat = {");
            depth++;
            if (cfg.PassiveSkillFamily.Count > 0)
            {
                passiveSkillPosition = 0;
                foreach (DataPackPassiveSkill passiveSkillFamily in cfg.PassiveSkillFamily)
                {
                    if (passiveSkillFamily.PassiveSkillFamily.Count > 0)
                    {
                        depth++;
                        tempScript = genDataPackPassiveSkill(passiveSkillFamily);
                        depth--;
                        if (tempScript != "")
                        {
                            if (passiveSkillPosition > 0)
                            {
                                script += ", ";
                            }
                            script += genNewLine(":" + genName(passiveSkillFamily.ID, passiveSkillFamily.Name) + " => {");
                            script += tempScript;
                            script += genNewLine("}");
                            passiveSkillPosition++;
                        }
                    }
                }
            }
            if (cfg.PassiveSkillID.Count > 0)
            {
                passiveSkillPosition = 0;
                foreach (DataPackPassiveSkill passiveSkillID in cfg.PassiveSkillID)
                {
                    depth++;
                    tempScript = genDataPackPassiveSkill(passiveSkillID);
                    depth--;
                    if (tempScript != "")
                    {
                        if (passiveSkillPosition > 0)
                        {
                            script += ", ";
                        }
                        script += genNewLine((passiveSkillID.ID).ToString() + " => {");
                        script += tempScript;
                        script += genNewLine("}");
                        passiveSkillPosition++;
                    }
                }
            }
            depth--;
            script += genNewLine("}");
            script += genNewLine("# Get the default stats of the passive skill family and passive skill id.");
            script += genNewLine("Default = {");
            depth++;
            tempScript = genDataPackPassiveSkill(cfg.PassiveSkillDefault);
            if (tempScript != "")
            {
                script += tempScript;
            }
            depth--;
            script += genNewLine("}");
            depth--;
            script += genNewLine("end");
            return script;
        }

        #endregion

        #region Weapon

        private static string genModuleWeapon()
        {
            string tempScript = "";
            int weaponPosition = 0;
            string script = genNewLine();
            script += genModuleHeader("Weapon", "weapon family, id and default");
            script += genNewLine("# Get the weapon in the weapon family.");
            script += genNewLine("Family = {");
            depth++;
            if (cfg.WeaponFamily.Count > 0)
            {
                weaponPosition = 0;
                foreach (DataPackEquipment weaponFamily in cfg.WeaponFamily)
                {
                    if (weaponFamily.WeaponFamily.Count > 0)
                    {
                        if (weaponPosition > 0)
                        {
                            script += ", ";
                        }
                        script += genNewLine(":" + genName(weaponFamily.ID, weaponFamily.Name) + " => [");
                        position = 0;
                        foreach (Weapon weapon in weaponFamily.WeaponFamily)
                        {
                            if (position > 0)
                            {
                                script += ", ";
                            }
                            script += weapon.ID.ToString();
                            position++;
                        }
                        script += "]";
                        weaponPosition++;
                    }
                }
            }
            depth--;
            script += genNewLine("}");
            script += genNewLine("# Get the stats of the weapon family and weapon id.");
            script += genNewLine("Stat = {");
            depth++;
            if (cfg.WeaponFamily.Count > 0)
            {
                weaponPosition = 0;
                foreach (DataPackEquipment weaponFamily in cfg.WeaponFamily)
                {
                    if (weaponFamily.WeaponFamily.Count > 0)
                    {
                        depth++;
                        tempScript = genDataPackWeapon(weaponFamily);
                        depth--;
                        if (tempScript != "")
                        {
                            if (weaponPosition > 0)
                            {
                                script += ", ";
                            }
                            script += genNewLine(":" + genName(weaponFamily.ID, weaponFamily.Name) + " => {");
                            script += tempScript;
                            script += genNewLine("}");
                            weaponPosition++;
                        }
                    }
                }
            }
            if (cfg.WeaponID.Count > 0)
            {
                weaponPosition = 0;
                foreach (DataPackEquipment weaponID in cfg.WeaponID)
                {
                    depth++;
                    tempScript = genDataPackWeapon(weaponID);
                    depth--;
                    if (tempScript != "")
                    {
                        if (weaponPosition > 0)
                        {
                            script += ", ";
                        }
                        script += genNewLine((weaponID.ID).ToString() + " => {");
                        script += tempScript;
                        script += genNewLine("}");
                        weaponPosition++;
                    }
                }
            }
            depth--;
            script += genNewLine("}");
            script += genNewLine("# Get the default stats of the weapon family and weapon id.");
            script += genNewLine("Default = {");
            depth++;
            tempScript = genDataPackWeapon(cfg.WeaponDefault);
            if (tempScript != "")
            {
                script += tempScript;
            }
            depth--;
            script += genNewLine("}");
            depth--;
            script += genNewLine("end");
            return script;
        }

        #endregion

        #region Armor

        private static string genModuleArmor()
        {
            string tempScript = "";
            int armorPosition = 0;
            string script = genNewLine();
            script += genModuleHeader("Armor", "armor family, id and default");
            script += genNewLine("# Get the armor in the armor family.");
            script += genNewLine("Family = {");
            depth++;
            if (cfg.ArmorFamily.Count > 0)
            {
                armorPosition = 0;
                foreach (DataPackEquipment armorFamily in cfg.ArmorFamily)
                {
                    if (armorFamily.ArmorFamily.Count > 0)
                    {
                        if (armorPosition > 0)
                        {
                            script += ", ";
                        }
                        script += genNewLine(":" + genName(armorFamily.ID, armorFamily.Name) + " => [");
                        position = 0;
                        foreach (Armor armor in armorFamily.ArmorFamily)
                        {
                            if (position > 0)
                            {
                                script += ", ";
                            }
                            script += armor.ID.ToString();
                            position++;
                        }
                        script += "]";
                        armorPosition++;
                    }
                }
            }
            depth--;
            script += genNewLine("}");
            script += genNewLine("# Get the stats of the armor family and armor id.");
            script += genNewLine("Stat = {");
            depth++;
            if (cfg.ArmorFamily.Count > 0)
            {
                armorPosition = 0;
                foreach (DataPackEquipment armorFamily in cfg.ArmorFamily)
                {
                    if (armorFamily.ArmorFamily.Count > 0)
                    {
                        depth++;
                        tempScript = genDataPackArmor(armorFamily);
                        depth--;
                        if (tempScript != "")
                        {
                            if (armorPosition > 0)
                            {
                                script += ", ";
                            }
                            script += genNewLine(":" + genName(armorFamily.ID, armorFamily.Name) + " => {");
                            script += tempScript;
                            script += genNewLine("}");
                            armorPosition++;
                        }
                    }
                }
            }
            if (cfg.ArmorID.Count > 0)
            {
                armorPosition = 0;
                foreach (DataPackEquipment armorID in cfg.ArmorID)
                {
                    depth++;
                    tempScript = genDataPackArmor(armorID);
                    depth--;
                    if (tempScript != "")
                    {
                        if (armorPosition > 0)
                        {
                            script += ", ";
                        }
                        script += genNewLine((armorID.ID).ToString() + " => {");
                        script += tempScript;
                        script += genNewLine("}");
                        armorPosition++;
                    }
                }
            }
            depth--;
            script += genNewLine("}");
            script += genNewLine("# Get the default stats of the armor family and armor id.");
            script += genNewLine("Default = {");
            depth++;
            tempScript = genDataPackArmor(cfg.ArmorDefault);
            if (tempScript != "")
            {
                script += tempScript;
            }
            depth--;
            script += genNewLine("}");
            depth--;
            script += genNewLine("end");
            return script;
        }

        #endregion

        #region Enemy

        private static string genModuleEnemy()
        {
            string tempScript = "";
            int enemyPosition = 0;
            string script = genNewLine();
            script += genModuleHeader("Enemy", "enemy family, id and default");
            script += genNewLine("# Get the enemy in the enemy family.");
            script += genNewLine("Family = {");
            depth++;
            if (cfg.EnemyFamily.Count > 0)
            {
                enemyPosition = 0;
                foreach (DataPackEnemy enemyFamily in cfg.EnemyFamily)
                {
                    if (enemyFamily.EnemyFamily.Count > 0)
                    {
                        if (enemyPosition > 0)
                        {
                            script += ", ";
                        }
                        script += genNewLine(":" + genName(enemyFamily.ID, enemyFamily.Name) + " => [");
                        position = 0;
                        foreach (Enemy enemy in enemyFamily.EnemyFamily)
                        {
                            if (position > 0)
                            {
                                script += ", ";
                            }
                            script += enemy.ID.ToString();
                            position++;
                        }
                        script += "]";
                        enemyPosition++;
                    }
                }
            }
            depth--;
            script += genNewLine("}");
            script += genNewLine("# Get the stats of the enemy family and enemy id.");
            script += genNewLine("Stat = {");
            depth++;
            if (cfg.EnemyFamily.Count > 0)
            {
                enemyPosition = 0;
                foreach (DataPackEnemy enemyFamily in cfg.EnemyFamily)
                {
                    if (enemyFamily.EnemyFamily.Count > 0)
                    {
                        depth++;
                        tempScript = genDataPackEnemy(enemyFamily);
                        depth--;
                        if (tempScript != "")
                        {
                            if (enemyPosition > 0)
                            {
                                script += ", ";
                            }
                            script += genNewLine(":" + genName(enemyFamily.ID, enemyFamily.Name) + " => {");
                            script += tempScript;
                            script += genNewLine("}");
                            enemyPosition++;
                        }
                    }
                }
            }
            if (cfg.EnemyID.Count > 0)
            {
                enemyPosition = 0;
                foreach (DataPackEnemy enemyID in cfg.EnemyID)
                {
                    depth++;
                    tempScript = genDataPackEnemy(enemyID);
                    depth--;
                    if (tempScript != "")
                    {
                        if (enemyPosition > 0)
                        {
                            script += ", ";
                        }
                        script += genNewLine((enemyID.ID).ToString() + " => {");
                        script += tempScript;
                        script += genNewLine("}");
                        enemyPosition++;
                    }
                }
            }
            depth--;
            script += genNewLine("}");
            script += genNewLine("# Get the default stats of the enemy family and enemy id.");
            script += genNewLine("Default = {");
            depth++;
            tempScript = genDataPackEnemy(cfg.EnemyDefault);
            if (tempScript != "")
            {
                script += tempScript;
            }
            depth--;
            script += genNewLine("}");
            depth--;
            script += genNewLine("end");
            return script;
        }

        #endregion

        #region State

        private static string genModuleState()
        {
            string tempScript = "";
            int statePosition = 0;
            string script = genNewLine();
            script += genModuleHeader("State", "state family, id and default");
            script += genNewLine("# Get the state in the state family.");
            script += genNewLine("Family = {");
            depth++;
            if (cfg.StateFamily.Count > 0)
            {
                statePosition = 0;
                foreach (DataPackState stateFamily in cfg.StateFamily)
                {
                    if (stateFamily.StateFamily.Count > 0)
                    {
                        if (statePosition > 0)
                        {
                            script += ", ";
                        }
                        script += genNewLine(":" + genName(stateFamily.ID, stateFamily.Name) + " => [");
                        position = 0;
                        foreach (State state in stateFamily.StateFamily)
                        {
                            if (position > 0)
                            {
                                script += ", ";
                            }
                            script += state.ID.ToString();
                            position++;
                        }
                        script += "]";
                        statePosition++;
                    }
                }
            }
            depth--;
            script += genNewLine("}");
            script += genNewLine("# Get the stats of the state family and state id.");
            script += genNewLine("Stat = {");
            depth++;
            if (cfg.StateFamily.Count > 0)
            {
                statePosition = 0;
                foreach (DataPackState stateFamily in cfg.StateFamily)
                {
                    if (stateFamily.StateFamily.Count > 0)
                    {
                        depth++;
                        tempScript = genDataPackState(stateFamily);
                        depth--;
                        if (tempScript != "")
                        {
                            if (statePosition > 0)
                            {
                                script += ", ";
                            }
                            script += genNewLine(":" + genName(stateFamily.ID, stateFamily.Name) + " => {");
                            script += tempScript;
                            script += genNewLine("}");
                            statePosition++;
                        }
                    }
                }
            }
            if (cfg.StateID.Count > 0)
            {
                statePosition = 0;
                foreach (DataPackState stateID in cfg.StateID)
                {
                    depth++;
                    tempScript = genDataPackState(stateID);
                    depth--;
                    if (tempScript != "")
                    {
                        if (statePosition > 0)
                        {
                            script += ", ";
                        }
                        script += genNewLine((stateID.ID).ToString() + " => {");
                        script += tempScript;
                        script += genNewLine("}");
                        statePosition++;
                    }
                }
            }
            depth--;
            script += genNewLine("}");
            script += genNewLine("# Get the default stats of the state family and state id.");
            script += genNewLine("Default = {");
            depth++;
            tempScript = genDataPackState(cfg.StateDefault);
            if (tempScript != "")
            {
                script += tempScript;
            }
            depth--;
            script += genNewLine("}");
            depth--;
            script += genNewLine("end");
            return script;
        }

        #endregion

        #endregion
    }
}