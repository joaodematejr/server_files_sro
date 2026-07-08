function QNO_WC_FERRY3_1()
  QUESTID = LuaGetQuestID("QNO_WC_FERRY3_1")
  LuaSetStartCodition(2, QSC_QUEST, QSC_LEVEL, 40, 30)
  QM_CONVERSATION = 1
  LuaSetStartMethod(QM_CONVERSATION, 2, "NPC_WC_FERRY3", "NPC_WC_SMITH")
  LuaQuestInsertNpc(2, "NPC_WC_FERRY3", "NPC_WC_SMITH")
  LuaInsertMissionOrCompleteNpc("NPC_WC_SMITH")
  LuaInsertMissionNPC("NPC_WC_SMITH")
  LuaSetAchievementLimit(1)
  LuaSetMissionDataSize(QUESTID, 1)
  LuaSetCollectionItemMissionData(QUESTID, 0, MISSION_TYPE_GATHER_ITEM_FROM_MONSTER, "SN_CON_QNO_WC_FERRY3_1", 1, "NPC_WC_SMITH", 1, 70, "ITEM_QNO_WC_FERRY3_1_01", "MOB_WC_HYEONGCHEON", 20)
  InsertQuestMenuStringList("NPC_WC_FERRY3", 3, "BASIC_MENUSTRING_GREETING", "SN_NPC_WC_FERRY3_QS", "BASIC_MENUSTRING_AT_ACCEPT", "SN_TALK_QNO_WC_FERRY3_1_02", "BASIC_MENUSTRING_ACHIEVED_NOW", "SN_TALK_QNO_WC_FERRY3_1_06")
  InsertQuestMenuStringList("NPC_WC_SMITH", 2, "BASIC_MENUSTRING_GREETING", "SN_NPC_WC_FERRY3_QS", "BASIC_MENUSTRING_AT_ACCEPT", "SN_TALK_QNO_WC_FERRY3_1_02")
  LuaInsertQuestFunctionStringList(1, "CONVERSATION_SINGLE", "QNO_WC_FERRY3_1_CONVERSATION")
  LuaSetMissionCompleteNum(0)
  PAY_ITEM_METHOD_EXACT = 1
  LuaSetAchievedItem(0, 516300, 0, 0, 0, 0)
end
function QNO_WC_FERRY3_1_CONVERSATION(QUESTID, EventID_Sub, CharName)
  if CharName == "NPC_WC_FERRY3" then
    QNO_WC_FERRY3_1_CONVERSATION_NPC_WC_FERRY3(QUESTID, EventID_Sub, CharName)
  elseif CharName == "NPC_WC_SMITH" then
    QNO_WC_FERRY3_1_CONVERSATION_NPC_WC_SMITH(QUESTID, EventID_Sub, CharName)
  end
end
function QNO_WC_FERRY3_1_CONVERSATION_NPC_WC_FERRY3(QUESTID, EventID_Sub, CharName)
  QuestStatus = LuaGetQuestStatus(QUESTID)
  if EventID_Sub == CONVERSATION_START then
    if QuestStatus == QUEST_STATUS_NODATA then
      CurPage = 1
      LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_WC_FERRY3_1_01", 1, 2, 0)
    elseif QuestStatus == QUEST_STATUS_ACHIEVING or QuestStatus == QUEST_STATUS_ACHIEVING_KILL_MONSTER or QuestStatus == QUEST_STATUS_ACHIEVED_KILL_MONSTER then
      if LuaGetStep() == 1 then
        CurPage = 0
        LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_WC_FERRY3_1_04", 0, 1, 0)
      else
        LuaSetPayStep(QUESTID, 1)
        LuaTerminateQuestMenu()
        LuaGObjAppearedInSight(QUESTID)
      end
    elseif QuestStatus == QUEST_STATUS_ACHIEVED_BUT_NOT_PAYED then
      CurPage = 3
      LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_WC_DOCTOR_3_05", 5, 1, 0)
    end
  elseif EventID_Sub == CONVERSATION_RESPONSE then
    if QuestStatus == QUEST_STATUS_NODATA then
      if LuaGetQuestCurPage() == 1 then
        MenuOffset = LuaGetQuestMenuResponse()
        MenuOffset = MenuOffset - TALK_RESPONSE_LIST_BASE
        if MenuOffset == 0 then
          if LuaPrepareQuestData(QUESTID, 0) == 1 then
            LuaQuestStart(QUESTID)
            LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_WC_FERRY3_1_02", 0, 1, 0)
          end
          LuaSetQuestCurPage(0)
        else
          CurPage = 2
          LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_WC_FERRY3_1_03", 0, 1, 0)
        end
      elseif LuaGetQuestCurPage() == 2 then
        LuaTerminateQuestMenu()
        LuaGObjAppearedInSight(QUESTID)
      end
    elseif LuaGetQuestCurPage() == 0 then
      LuaTerminateQuestMenu()
      LuaGObjAppearedInSight(QUESTID)
    end
  elseif EventID_Sub == CONVERSATION_TERMINATE then
    LuaGObjAppearedInSight(QUESTID)
  end
end
function QNO_WC_FERRY3_1_CONVERSATION_NPC_WC_SMITH(QUESTID, EventID_Sub, CharName)
  QuestStatus = LuaGetQuestStatus(QUESTID)
  if EventID_Sub == CONVERSATION_START then
    if QuestStatus == QUEST_STATUS_ACHIEVED_KILL_MONSTER then
      if LuaCheckQuestAchieveCondition(QUESTID) == 1 and LuaGetStep() == 1 then
        CurPage = 3
        LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_WC_FERRY3_1_05", 5, 1, 1)
      end
    elseif QuestStatus == QUEST_STATUS_ACHIEVED_BUT_NOT_PAYED then
      CurPage = 3
      LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_WC_FERRY3_1_03", 5, 1, 1)
    else
      CurPage = 0
      LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_WC_FERRY3_1_07", 0, 1, 1)
    end
  elseif EventID_Sub == CONVERSATION_RESPONSE then
    if LuaGetQuestCurPage() == 0 then
      LuaTerminateQuestMenu()
    elseif LuaGetQuestCurPage() == 3 then
      Result = 0
      Result = LuabSetPayQuest(QUESTID)
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
