using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnShop.Framework.Domain;

namespace OnShop.Domain.Wallet.Entities
{
    public class WalletType : BaseHcEntity<long>
    {

        #region Relations
        public virtual List<Wallet> Wallets { get; set; }
        #endregion
    }
}
