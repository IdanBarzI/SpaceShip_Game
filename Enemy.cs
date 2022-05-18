using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameNew
{
    public class Enemy : GameElem
    {
        protected int Damage { get; set; }
        protected int Score { get; set; }
       
        public int GetDamage()
        {
            return this.Damage;
        }
        
        public int GetScore()
        {
            return this.Score;
        }

    }
}
