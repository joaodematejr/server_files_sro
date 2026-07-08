function QNO_WC_FERRY4_1()
  QUESTID = LuaGetQuestID("QNO_WC_FERRY4_1")
  LuaSetStartCodition(2, QSC_QUEST, QSC_LEVEL, 35, 25)
  QM_CONVERSATION = 1
  LuaSetAchievementLimit(1)
  LuaSetStartMethod(QM_CONVERSATION, 1, "NPC_WC_FERRY4")
  LuaQuestInsertNpc(1, "NPC_WC_FERRY4")
  LuaInsertMissionOrCompleteNpc("NPC_WC_FERRY4")
  InsertQuestMenuStringList("NPC_WC_FERRY4", 1, "BASIC_MENUSTRING_GREETING", "SN_NPC_WC_FERRY3_QS")
  LuaInsertQuestFunctionStringList(1, "CONVERSATION_SINGLE", "QNO_WC_FERRY4_1_CONVERSATION")
  LuaSetMissionCompleteNum(0)
  PAY_ITEM_METHOD_EXACT = 1
end
function QNO_WC_FERRY4_1_CONVERSATION(QUESTID, EventID_Sub, CharName)
  QuestStatus = LuaGetQuestStatus(QUESTID)
  if EventID_Sub == CONVERSATION_START then
    if QuestStatus == QUEST_STATUS_NODATA then
      CurPage = 1
      LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_WC_FERRY4_1_01", 1, 2, 0)
    elseif QuestStatus == QUEST_STATUS_ACHIEVED_BUT_NOT_PAYED then
      CurPage = 3
      LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_WC_FERRY4_1_01", 5, 1, 0)
    end
  elseif EventID_Sub == CONVERSATION_RESPONSE then
    if QuestStatus == QUEST_STATUS_NODATA then
      if LuaGetQuestCurPage() == 1 then
        MenuOffset = LuaGetQuestMenuResponse()
        MenuOffset = MenuOffset - TALK_RESPONSE_LIST_BASE
        if MenuOffset == 0 then
          ChkErroCode = 0
          ChkErroCode = LuaQuestAddItem_EXT(QUESTID, 0, "ITEM_QNO_WC_FERRY4_1_01", 1, SYSOP_REASON_QUEST, 0, 1, 0)
          if ChkErroCode ~= 1 then
            CurPage = 2
            LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_WC_FERRY4_1_02", 0, 1, 0)
          else
            CurPage = 10
            LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_WC_FERRY4_1_03", 4, 1, 0)
          end
        else
          CurPage = 2
          LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_WC_FERRY4_1_07", 0, 1, 0)
        end
      elseif LuaGetQuestCurPage() == 2 then
        LuaTerminateQuestMenu()
        return
      elseif LuaGetQuestCurPage() == 10 then
        CurPage = 20
        LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_WC_FERRY4_1_04", 4, 1, 0)
      elseif LuaGetQuestCurPage() == 20 then
        CurPage = 30
        LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_WC_FERRY4_1_05", 4, 1, 0)
      elseif LuaGetQuestCurPage() == 30 then
        CurPage = 31
        LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_WC_FERRY4_1_06", 0, 1, 0)
      elseif LuaGetQuestCurPage() == 31 then
        Result = 0
        LuaSaveQuestNow(QUESTID)
        LuaQuestGiveEventPoint_EXP_Gold(650, 0, 0)
        ChkErroCode = LuaQuestAddItem_EXT(QUESTID, 0, "ITEM_QNO_WC_FERRY4_1_01", 1, SYSOP_REASON_QUEST, 0, 1, 0)
        if ChkErroCode ~= 1 then
          LuaQuestShowMenuCommon(CurPage, QUESTID, "SN_TALK_QNO_SD_MA_033_05", 0, 1, 0)
        else
          LuaQuestAddItem_EXT(QUESTID, 0, "ITEM_QNO_WC_FERRY4_1_01", 1, SYSOP_REASON_QUEST, 0, 0, 0)
          LuaSendQuestEventMessage("SN_TALK_QNO_WC_FERRY4_1_08")
        end
        LuaGObjAppearedInSight(QUESTID)
        LuaTerminateQuestMenu()
      end
    end
  elseif EventID_Sub == CONVERSATION_TERMINATE then
    LuaGObjAppearedInSight(QUESTID)
  end
end
