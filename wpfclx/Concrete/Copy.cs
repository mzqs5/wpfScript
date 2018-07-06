using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfclx.Abstract;

namespace wpfclx.Concrete
{
    public class Copy : ICopy
    {
        public Copy(IntPtr handle)
        {
            this.handle = handle;
        }

        public IntPtr handle { get; set; }

        public void AutoMatch()
        {
            throw new NotImplementedException();
        }

        public void CreateTeam()
        {
            throw new NotImplementedException();
        }

        public void Dialogue()
        {
            throw new NotImplementedException();
        }

        public void GoTo()
        {
            throw new NotImplementedException();
        }

        public void QuitTeam()
        {
            throw new NotImplementedException();
        }
    }
}
