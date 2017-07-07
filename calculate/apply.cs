using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace calculate
{
    public class apply
    {
        public int x;
        public int y;
        public int z;
        public int[] a=  new int[20];//数组各个区间默认值是0；
        public apply(int m,int n)//apply类的有参构造方法
        {
            x = m;
            y = n;
            if (y >= 18)//设定上限
               y = 18;
           if (x <= 7)//设定下限
               x = 7;       
        }
        public int[]  show()
        {
            if (y-x==1)//差值为1
            {
                z = x - 7;
                a[z] = 1; 
                return a;
            }                                                    
            else //差值不为1
            {
                int n = y - x;
                z = x - 7;
                n = n + z;
                for(int i=z;i<n;i++)
                {
                    a[i] = a[i] + 1;      //将数组又开始时段到结束时段，每个部分由0变为1
                }
                return a;
            }
        }
    }
}
