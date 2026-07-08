function QNO_KT_POTION_2()
  QUESTID = LuaGetQuestID("QNO_KT_POTION_2")
  LuaSetStartCodition(2, QSC_QUEST, QSC_LEVEL, 77, 67)
  QM_CONVERSATION = 1
  LuaSetStartMethod(QM_CONVERSATION, 1, "NPC_KT_POTION")
  LuaInsertMissionOrCompleteNpc("NPC_KT_POTION")
  LuaQuestInsertNpc(1, "NPC_KT_POTION")
  LuaSetAchievementLimit(3)
  LuaSetMissionDataSize(QUESTID, 1)
  LuaSetCollectionItemMissionData(QUESTID, 0, MISSION_TYPE_GATHER_ITEM_FROM_MONSTER, "SN_CON_QNO_KT_POTION_2", 2, "NPC_KT_POTION", 1, 100, "ITEM_QNO_KT_POTION_2_01", "MOB_TK_BONEMAGE_CLON", 20, "MOB_TK_BONEMAGE", 20)
  InsertQuestMenuStringList("NPC_KT_POTION", 7, "BASIC_MENUSTRING_GREETING", "SN_NPC_KT_POTION_QS", "BASIC_MENUSTRING_REQUEST_ACCEPT_QUEST", "SN_TALK_QNO_KT_POTION_2_02", "BASIC_MENUSTRING_AT_ACCEPT", "SN_TALK_QNO_KT_POTION_2_03", "BASIC_MENUSTRING_AT_DENY", "SN_TALK_QNO_KT_POTION_2_04", "BASIC_MENUSTRING_NOT_ACHIEVED", "SN_TALK_QNO_KT_POTION_2_05", "BASIC_MENUSTRING_ACHIEVED", "SN_TALK_QNO_KT_POTION_2_06", "BASIC_MENUSTRING_ACHIEVED_NOW", "SN_TALK_QNO_KT_POTION_2_07")
  LuaSetMissionCompleteNum(0)
  PAY_ITEM_METHOD_EXACT = 1
  LuaSetAchievedItem(0, 8731000, 0, 350000, 0, 0)
  LuaInsertQuestFunctionStringList(1, "CONVERSATION_SINGLE", "QNO_KT_POTION_2_CONVERSATION")
end
function QNO_KT_POTION_2_CONVERSATION(QUESTID, EventID_Sub, CharName)
  QuestStatus = LuaGetQuestStatus(QUESTID)
  if EventID_Sub == CONVERSATION_START then
    if QuestStatus == QUEST_STATUS_NODATA then
      CurPage = 1
      LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_KT_POTION_2_01", 4, 1, 0)
    elseif QuestStatus == QUEST_STATUS_ACHIEVING or QuestStatus == QUEST_STATUS_ACHIEVING_KILL_MONSTER or QuestStatus == QUEST_STATUS_ACHIEVED_KILL_MONSTER then
      if LuaGetStep() == 1 then
        if LuaCheckQuestAchieveCondition(QUESTID) == 0 then
          CurPage = 0
          LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_KT_POTION_2_05", 0, 1, 0)
        else
          LuaSetPayStep(QUESTID, 1)
          LuaSaveLocalQuestNow(QUESTID)
          if LuaEnablePayQuestItem(QUESTID) == true then
            CurPage = 4
            LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_KT_POTION_2_06", 5, 1, 0)
          else
            CurPage = 4
            LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_KT_POTION_2_06", 5, 1, 0)
          end
        end
      else
        LuaSetPayStep(QUESTID, 1)
        LuaTerminateQuestMenu()
        LuaGObjAppearedInSight(QUESTID)
      end
    elseif QuestStatus == QUEST_STATUS_ACHIEVED_BUT_NOT_PAYED then
      CurPage = 3
      LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_KT_POTION_2_06", 5, 1, 0)
    end
  elseif EventID_Sub == CONVERSATION_RESPONSE then
    if QuestStatus == QUEST_STATUS_NODATA then
      if LuaGetQuestCurPage() == 1 then
        CurPage = 2
        LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_KT_POTION_2_02", 1, 2, 0)
      elseif LuaGetQuestCurPage() == 2 then
        CurPage = 3
        MenuOffset = LuaGetQuestMenuResponse()
        MenuOffset = MenuOffset - TALK_RESPONSE_LIST_BASE
        if MenuOffset == 0 then
          if LuaPrepareQuestData(QUESTID, 0) == 1 then
            LuaQuestStart(QUESTID)
            LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_KT_POTION_2_03", 0, 1, 0)
          end
        else
          LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_KT_POTION_2_04", 0, 1, 0)
        end
        return
      elseif LuaGetQuestCurPage() == 3 then
        LuaTerminateQuestMenu()
        return
      end
    else
      Result = 0
      if LuaGetQuestCurPage() == 4 then
        Result = LuabSetPayQuest(QUESTID)
      end
      LuaTerminateQuestMenu()
      if Result == 1 then
        LuaSendQuestEventMessage("SN_TALK_COMMON_END")
      end
      LuaGObjAppearedInSight(QUESTID)
    end
  elseif EventID_Sub == CONVERSATION_TERMINATE then
    LuaGObjAppearedInSight(QUESTID)
  end
end
