using MultiCoreApp.Core.IntRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCoreApp.Core.IntUnitOfWork
{
    public interface IUnitOfWork//buda bir patttern dir
    {
        //changetrucker:sizin sisteminizi takip ediyo

        IProductRepository Product{ get; }
        ICategoryRepository Category { get; }
        Task CommitAsync(); //add için bunu kullanıcaz ,savechange işlemimiz olucak
        void Commit(); //update ve remove için bunu
    }
}
