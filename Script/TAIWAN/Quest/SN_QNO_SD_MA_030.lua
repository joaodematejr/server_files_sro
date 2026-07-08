function QNO_SD_MA_030()
  QUESTID = LuaGetQuestID("QNO_SD_MA_030")
  LuaSetStartCodition(2, QSC_QUEST, QSC_LEVEL, 110, 100)
  QM_CONVERSATION = 1
  LuaSetStartMethod(QM_CONVERSATION, 1, "NPC_SD_M_AREA_SENMUTE")
  LuaInsertMissionOrCompleteNpc("NPC_SD_M_AREA_SENMUTE")
  LuaQuestInsertNpc(1, "NPC_SD_M_AREA_SENMUTE")
  LuaInsertDependancyQuests(1, "QNO_SD_MA_029")
  LuaSetMissionDataSize(QUESTID, 1)
  LuaSetCollectionItemMissionData(QUESTID, 0, MISSION_TYPE_GATHER_ITEM_FROM_MONSTER, "SN_CON_QNO_SD_MA_030_1", 1, "NPC_SD_M_AREA_SENMUTE", 1, 1, "ITEM_QNO_SD_MA_030_2", "MOB_SD_NEITH", 0)
  InsertQuestMenuStringList("NPC_SD_M_AREA_SENMUTE", 8, "BASIC_MENUSTRING_GREETING", "SN_NPC_SD_M_AREA_SENMUTE_QS", "BASIC_MENUSTRING_REQUEST_ACCEPT_QUEST", "SN_TALK_QNO_SD_MA_030_01", "BASIC_MENUSTRING_AT_ACCEPT", "SN_TALK_QNO_SD_MA_030_02", "BASIC_MENUSTRING_AT_DENY", "SN_TALK_QNO_SD_MA_030_03", "BASIC_MENUSTRING_NOT_ACHIEVED", "SN_TALK_QNO_SD_MA_030_04", "BASIC_MENUSTRING_INVENTORY_FULL", "SN_TALK_QNO_SD_MA_030_05", "BASIC_MENUSTRING_ACHIEVED", "SN_TALK_QNO_SD_MA_030_06", "BASIC_MENUSTRING_ACHIEVED_NOW", "SN_TALK_QNO_SD_MA_030_07")
  LuaSetAchievementLimit(1)
  LuaSetMissionCompleteNum(0)
  PAY_ITEM_METHOD_EXACT = 1
  LuaSetAchievedItem(0, 75300714, 0, 15356, 0, 0)
  LuaSetAchievedSkillPont(4100)
  CONVERSATION_SINGLE = 0
  LuaInsertQuestFunctionStringList(6, "CONVERSATION_SINGLE", "QNO_CH_QUEST_CONVERSATION_SD_MA_030", "ABORTQUEST", "AbortQuest_SD_MA_030", "ENDQUEST", "EndQuest_SD_MA_030", "RESTARTQUEST", "RestartQuest_SD_MA_030", "STARTQUEST", "StartQuest_SD_MA_030", "UNIQUE_DEAD", "QNO_UNIQUE_DEAD_MA_030")
end
function AbortQuest_SD_MA_030()
  LuaQuestSetDeleteHandler(QUESTID, "UNIQUE_DEAD")
  item_count = LuaInQuireSameItem(0, "ITEM_QNO_SD_MA_030_1", INQUIRE_SAMEITEM_OP_COUNT_FIRST_ITEM, -1)
  if item_count ~= 0 then
    slot = LuaInQuireSameItem(0, "ITEM_QNO_SD_MA_030_1", INQUIRE_SAMEITEM_OP_FIND_FIRST_SLOT, -1)
    LuaDelItemEXT(0, slot, item_count, SYSOP_REASON_QUEST, FALSE)
    if Result ~= STRGERR_OPERATION_SUCCEEDED then
      return
    end
  end
end
function EndQuest_SD_MA_030()
  QUESTID = LuaGetQuestID("QNO_SD_MA_030")
  LuaQuestSetDeleteHandler(QUESTID, "UNIQUE_DEAD")
end
function RestartQuest_SD_MA_030()
  QUESTID = LuaGetQuestID("QNO_SD_MA_030")
  LuaQuestSetStartHandler(QUESTID, "UNIQUE_DEAD", HANDLE_TYPE_QUEST, EVENT_UNIQUE_DEAD)
end
function StartQuest_SD_MA_030()
  QUESTID = LuaGetQuestID("QNO_SD_MA_030")
  LuaQuestSetStartHandler(QUESTID, "UNIQUE_DEAD", HANDLE_TYPE_QUEST, EVENT_UNIQUE_DEAD)
end
function QNO_UNIQUE_DEAD_MA_030()
  QUESTID = LuaGetQuestID("QNO_SD_MA_030")
  QuestStatus = LuaGetQuestStatus(QUESTID)
  if QuestStatus == QUEST_STATUS_NODATA then
    return
  end
  if LuaGetCurRegionID(QUESTID, "PLAYER") ~= -32757 then
    return
  end
  if LuaCmpCodeName("UNIQUE_MOB", "MOB_SD_NEITH") ~= 0 then
    return
  end
  if 0 < LuaInQuireSameItem(0, "ITEM_QNO_SD_MA_030_1", INQUIRE_SAMEITEM_OP_COUNT_ALL_SAMEITEM, 0) then
    slot = LuaInQuireSameItem(0, "ITEM_QNO_SD_MA_030_1", INQUIRE_SAMEITEM_OP_FIND_FIRST_SLOT, 0)
    Result = LuaDelItemEXT(0, slot, 1, SYSOP_REASON_QUEST, 0)
    if Result == 0 then
      return
    end
    Result = LuaQuestAddItem_EXT(QUESTID, 0, "ITEM_QNO_SD_MA_030_2", 1, SYSOP_REASON_QUEST, 0, 0, 0)
    if Result == STRGERR_OPERATION_SUCCEEDED then
      return
    end
  end
end
function QNO_CH_QUEST_CONVERSATION_SD_MA_030(QUESTID, EventID_Sub, CharName)
  QuestStatus = LuaGetQuestStatus(QUESTID)
  if EventID_Sub == CONVERSATION_START then
    if QuestStatus == QUEST_STATUS_NODATA then
      CurPage = CUR_PAGE_01
      LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_SD_MA_030_01", 1, 2, 0)
    elseif QuestStatus == QUEST_STATUS_ACHIEVING or QuestStatus == QUEST_STATUS_ACHIEVING_KILL_MONSTER or QuestStatus == QUEST_STATUS_ACHIEVED_KILL_MONSTER then
      if LuaCheckQuestAchieveCondition(QUESTID) == 0 then
        CurPage = 0
        LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_SD_MA_030_04", 0, 1, 0)
      else
        LuaSetPayStep(QUESTID, 1)
        LuaSaveLocalQuestNow(QUESTID)
        CurPage = 3
        LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_SD_MA_030_06", 5, 1, 0)
      end
    end
    return
  end
  if EventID_Sub == CONVERSATION_RESPONSE then
    if QuestStatus == QUEST_STATUS_NODATA then
      if LuaGetQuestCurPage() == CUR_PAGE_01 then
        CurPage = CUR_PAGE_02
        MenuOffset = LuaGetQuestMenuResponse()
        MenuOffset = MenuOffset - TALK_RESPONSE_LIST_BASE
        if MenuOffset == 0 then
          ChkErroCode = 0
          ChkErroCode = LuaQuestAddItem_EXT(QUESTID, 0, "ITEM_QNO_SD_MA_030_1", 1, SYSOP_REASON_QUEST, 0, 1, 0)
          if ChkErroCode ~= 1 then
            LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_SD_MA_030_05", 0, 1, 0)
          else
            LuaQuestAddItem_EXT(QUESTID, 0, "ITEM_QNO_SD_MA_030_1", 1, SYSOP_REASON_QUEST, 0, 0, 0)
            if LuaPrepareQuestData(QUESTID, 0) == 1 then
              LuaQuestStart(QUESTID)
              LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_SD_MA_030_02", 0, 1, 0)
            end
          end
        else
          LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_SD_MA_030_03", 0, 1, 0)
        end
        return
      end
      if LuaGetQuestCurPage() == CUR_PAGE_02 then
        LuaTerminateQuestMenu()
        return
      end
    else
      Result = 0
      if LuaGetQuestCurPage() == CUR_PAGE_03 then
        Result = LuabSetPayQuest(QUESTID)
      end
      LuaTerminateQuestMenu()
      if Result == 1 then
        LuaSendQuestEventMessage("SN_TALK_COMMON_END")
      end
      LuaGObjAppearedInSight(QUESTID)
    end
    return
  end
  if EventID_Sub == CONVERSATION_TERMINATE then
    LuaGObjAppearedInSight(QUESTID)
  end
end
