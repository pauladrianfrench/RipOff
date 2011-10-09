using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipOff
{
    public class Mission : IMission
    {
        public Mission()
        {
            this.Targets = new List<IMissionTarget>();
        }

        public List<IMissionTarget> Targets { get; set; }
        public bool Complete { get { return this.GetNextUncompletedTarget() == null; } }

        public IMissionTarget GetNextUncompletedTarget()
        {
            for (int i = 0; i < Targets.Count; i++)
            {
                if (!Targets[i].Complete)
                {
                    return Targets[i];
                }
            }
            return null;
        }
    }
}
