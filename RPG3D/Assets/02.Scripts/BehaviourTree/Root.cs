﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULB.RPG.AISystems
{
    public class Root : Behaviour, IChild
    {
        public Behaviour child { get; set; }
        public Behaviour running;

        public Result Invoke()
        {
            Result result;
            Behaviour leaf;

            // running 을 반환했던 leaf behavior 가 있으면 그걸 실행.
            if (running == null)
            {
                result = child.Invoke(out leaf);
            }
            else
            {
                result = running.Invoke(out leaf);
            }

            // running 이 반환되면 leaf behavior 저장.
            if (result == Result.Running)
            {
                running = leaf;
            }
            else
            {
                running = null;
            }

            return result;
        }

        public override Result Invoke(out Behaviour leaf)
        {
            return child.Invoke(out leaf);
        }
    }
}
