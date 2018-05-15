using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
    public class SysVarietyModel
    {
        /// <summary>
        /// 品种编码
        /// </summary>
        public string VarietyCode { get; set; }
        /// <summary>
        /// 品种名称
        /// </summary>
        public string VarietyName { get; set; }
    }
    public class SysCodeModel
    {
        /// <summary>
        /// 合约唯一标识
        /// </summary>
        public string SystemName { get; set; }
        /// <summary>
        /// 品种合约代码
        /// </summary>
        public string SysVarietyCode { get; set; }
    }
}
