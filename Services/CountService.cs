using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sgr
{
    /// <summary>
    /// 服务，业务逻辑在此处
    /// </summary>
    public class CountService
    {
        private int _count;     //成员变量
        public int GetLatestCount()
        {
            return _count++;    //自增
        }
    }
}
