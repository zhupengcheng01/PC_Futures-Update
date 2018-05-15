namespace Futures.Enum
{
    public enum DeleteType
    {
        /// <summary>
        /// //委托状态 1 表示委托已发送到服务器；
        /// </summary>
        ComitServer = 1,
        /// <summary>
        ///  // 2委托创建成功(委托已在交易交易所创建成功)；
        /// </summary>
        CreateSuccess = 2,
        /// <summary>
        /// // 3委托全部生效；
        /// </summary>
        AllTakeEffect = 3,
        /// <summary>
        ///  // 4委托部分生效，剩余部分还在委托队列中；
        /// </summary>
        PortionTakeEffect = 4,
        /// <summary>
        ///  // 5委托部分生效，剩余委托被用户撤销；
        /// </summary>
        PortionTakeEffectCannel = 5,
        /// <summary>
        ///   // 6委托部分生效，剩余委托被系统撤销；
        /// </summary>
        PortionTakeEffectSystemCannel = 6,
        /// <summary>
        ///   // 7委托未生效，用户撤销；
        /// </summary>
        UnTakeEffecUserCannel = 7,
        /// <summary>
        ///   // 8委托未生效，被系统撤销
        /// </summary>
        UnTakeEffecSysCannel = 8,
    }
    public enum SysSettleType
    {
        Sys_SettleDate = 0,             //日结算
        Sys_SettleMonth = 1,            //月结算
        Sys_SettleByRange = 2,          //按时间段
    };
    public enum RiskUpDownOperatorType
    {
        R_QuotationUpDown = 1,        // 涨跌幅
    };

    public enum RiskUpDownOrderLimitType
    {
        R_OrderOpenEnable = 0,        //允许开仓
        R_OrderOpenProhibit = 1,      //禁止开仓
        R_OrderReversedProhibit = 2,  //禁止开反向仓
        R_OrderCloseProhibit = 3,     //禁止平仓
    };

    public enum RiskUpDownPositionAdjustType
    {
        R_PositionNoAdjust = 0,       //不调整
        R_PositionFullClose = 1,      //全部清仓
        R_PositionReverseClose = 2,   //清反向仓
    };

    public enum RiskModuleType
    {
        R_SetMoneyIndex = 1,          //资金指标设置
        R_SetTimingClose = 2,         //定时强平设置
        R_SetOverNightClose = 3,      //隔夜强平设置
        R_SetTradeProdouctClose = 4,      //交易品种及数量设置
    };


    //资金指标
    public enum RiskMoneyIndexType
    {
        R_PriorityMoneyIndex = 1,        // 按优先资金
        R_InferiorMoneyIndex = 2,        // 按劣后资金
        R_RiskDegreeIndex = 3,           // 按风险度
        R_TodayLossIndex = 4,            // 按当日亏损
        R_DynamicEquityIndex = 5,        // 按动态权益
        R_InferiorRiskDegreeIndex = 6,   // 按劣后风险度
    };

    // 账户属性
    public enum E_ACCOUNT_PROP
    {
        EAP_Invalid = -1,
        EAP_Root,                           // 根账户
        EAP_Trader,                         // 交易账户
        EAP_Risker,                         // 风控账户
        EAP_Manager,                        // 管理账户
        EAP_Agency,                         // 机构账户
        EAP_Funder,                         // 资金账户
        EAP_MAX,
    };
    // 账户状态
    public enum E_ACCOUNT_STATUS
    {
        EAS_Invalid = -1,
        EAS_LOGIN,                          // 登录
        EAS_UNLOGIN,                        // 未登录
    };
    // 账户类型
    public enum E_ACCOUNT_TYPE
    {
        EAT_Invalid = -1,
        EAT_Simulation,                     // 模拟账户
        EAT_Real,                           // 实盘账户
        EAT_SimuReal,                       // 模拟或实盘账户
        EAT_MaxCount
    };
    //下单类型
    public enum OrderStyle
    {
        LASTPRICE = 0,                    // 最新价
        BUYONEPRICE = 1,                  // 买一价
        SELLONEPRICE = 2,                //  卖一价
        MARKETPRICE = 3,                 //  市价
    };
    //条件单触发类型
    public enum YunTrrigerStyle
    {
        Y_LASTPRICE = 0,                     // 最新价
        Y_BUYONEPRICE = 1,                   // 买一价
        Y_SELLONEPRICE = 2,                  // 卖一价
    };
    //开平类型
    public enum OffsetType
    {
        OFFSET_Invalid = -1,
        OFFSET_OPEN = 1,                     // 开仓
        OFFSET_COVER = 2,                        // 平仓
        OFFSET_COVER_TODAY = 3,                  // 平今
    };
    //委托状态类型
    public enum OrderStateType
    {
        ORDER_STATE_Invalid = -1,
        ORDER_STATE_FAIL = 0,               // 指令失败
        ORDER_STATE_ACCEPT,                 // 已受理,加入至服务器队列
        ORDER_STATE_QUEUED,                 // 已排队,加入至竞价/交易队列/提交至交易所
        ORDER_STATE_SUPPENDING,             // 提交中
        ORDER_STATE_SUPPENDED,              // 已挂起
        ORDER_STATE_FINISHED,               // 完全成交
        ORDER_STATE_PARTFINISHED,           // 部分成交
        ORDER_STATE_DELETEING,              // 待撤消
        ORDER_STATE_PARTDELETED,            // 部分撤单
        ORDER_STATE_DELETED,                // 完全撤单

    };
    //触发类型
    public enum YunTriggerType
    {
        Y_GREATEREQUAL = 0,                 //  大于等于
        Y_LESSEREQUAL = 1,                  //  小于等于
    };
    // 时间类型
    public enum TimeType
    {
        T_type_Invalid = -1,
        T_CurrentDay,                         // 当日有效
        T_PromptDay,                          // 交割日钱有效
    };
    //条件单触发类型
    public enum YunConditonOrderType
    {
        Y_GREATEREQUALLASTPRICE = 0,           // 最新价 大于等于
        Y_LESSEQUALLASTPRICE = 1,              // 最新价 小于等于
        Y_GREATEREQUALBUYONEPRICE = 2,         // 买一价 大于等于
        Y_LESSEQUALBUYONEPRICE = 3,            // 买一价 小于等于
        Y_GREATEREQUALSELLONEPRICE = 4,       //  卖一价 大于等于
        Y_LESSEQUALSELLONEPRICE = 5,          //  卖一价 小于等于
    };
    //操作类型
    public enum YunOperatorType
    {
        Y_ADDOPERATOR = 0,                   // 增加
        Y_UPDATEOPERATOR = 1,                // 修改
        Y_DELETEOPERATOR = 2,                // 删除
    };
    //条件单类型
    public enum YunConditionType
    {
        Y_PRICE = 0,                         //价格
        Y_TIME = 1,                         //时间
        Y_TIMEPRICE = 2,                     //时间价格
    };

    // 条件单状态
    public enum ConditionStateType
    {
        CONDITION_STATE_Invalid = -1,
        CONDITION_STATE_WORKING,          // 新建(工作中)
        CONDITION_STATE_HADTRRIGER,       // 已触发
        CONDITION_STATE_FAIL,             // 失败
        CONDITION_STATE_DELETE,           // 删除


    };
    //操作员类型
    public enum OperatorTradeType
    {
        OPERATOR_TRADE_Invalid = -1,
        OPERATOR_TRADE_PC,              // PC端客户自发起
        OPERATOR_TRADE_APP,             // APP端客户自发起
        OPERATOR_TRADE_MC,              // 后台监控端发起
        OPERATOR_TRADE_RC,              // 后台风控端发起
        OPERATOR_TRADE_SYSTEM,          // 系统发起
        OPERATOR_WEB,                   // 网页端发起
    };
    // 出入金方式
    public enum OIMM_Change_Type
    {
        OIMM_Type_Invalid = -1,
        OIMM_In_WithCapotal,            // 配资入金
        OIMM_Out_Withdrawals,           // 出金提现
    };
    // 资金变动方式
    public enum Money_Change_Type
    {
        MC_Type_Invalid = -1,
        MC_In_Deposit,                  // 加劣后资金(追加保证金)
        MC_In_Equipment,                // 加优先资金
        MC_In_BlueAdd,                  // 资金蓝补
        MC_Out_Deposit,                 // 扣劣后资金
        MC_Out_Equipment,               // 扣优先资金(减少配资)
        MC_Out_RedSub,                  // 资金红冲		
        MC_Order_Freeze,                // 开仓委托冻结资金,可用资金减少
        MC_Order_Cancel,                // 撤单,冻结资金返还
        MC_Trade_Open,                  // 开仓成交(成交价)
        MC_Trade_Open_Fee,              // 开仓成交交易佣金手续费
        MC_Trade_Open_TransferFee,      // 开仓成交过户费
        MC_Trade_Open_InfoMatchFee,     // 开仓成交信息撮合费
        MC_Trade_Thaw,                  // 委托冻结资金返还
        MC_Trade_Close,                 // 平仓成交资金返还(成交价)
        MC_Trade_Close_Fee,             // 平仓成交交易佣金手续费
        MC_Trade_Close_TransferFee,     // 平仓成交过户费
        MC_Trade_Close_InfoMatchFee,    // 平仓成交信息撮合费
        MC_Trade_Close_StampFee,        // 平仓成交印花税
        MC_Commission,                  // 系统返佣
        MC_Money_Settle,                // 系统资金结算,改为可提取资金
        MC_Profit,                      // 系统分成抽取
        MC_In_WithCapotal,              // 配资入金
        MC_Out_Withdrawals,             // 出金提现
        MC_SecLendProfit,               // 融券开仓成交收取利息
        MC_ProfitUseT,                  // 递延费收取利息
        MC_Type_Num
    };
    //接口类型类型
    public enum ApiTypeType
    {
        APIType_TYPE_Invalid = -1,
        APIType_TYPE_CTP = 0,
        APIType_TYPE_ESUNNY,
        APIType_TYPE_IB,
        APIType_TYPE_SP,
    };
    // 层级
    public enum LevelType
    {
        LType_Invalid = -1,
        LType_Institution = 1,      // 机构
        LType_AgentOne = 2,         // 一级代理
        LType_AgentTwo = 3,         // 二级代理
    };
    // 手续费计算方式
    public enum FeeCalcType
    {
        FCT_Invalid = -1,
        FCT_Fixed,                // 固定
        FCT_Ratio,                // 比例
    };

    //保证金计算方式
    public enum MarginCalcType
    {
        MCT_Invalid = -1,//--原先是0固定值，1比例，现按要求改变0是比例，1是固定值
                         //MCT_Fixed,              // 固定
                         //MCT_Ratio,              // 比例
        MCT_Ratio,              // 比例
        MCT_Fixed,              // 固定     
    };
    //浮动止损标志
    public enum FloatLossFlag
    {
        FLF_Invalid = -1,
        FLF_FloatLoss,        // 浮动止损
        FLF_NoFloatLoss,      // 无浮动止损
    };
}
