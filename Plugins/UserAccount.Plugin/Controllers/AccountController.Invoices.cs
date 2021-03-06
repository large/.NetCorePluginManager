﻿/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 *  .Net Core Plugin Manager is distributed under the GNU General Public License version 3 and  
 *  is also available under alternative licenses negotiated directly with Simon Carter.  
 *  If you obtained Service Manager under the GPL, then the GPL applies to all loadable 
 *  Service Manager modules used on your system as well. The GPL (version 3) is 
 *  available at https://opensource.org/licenses/GPL-3.0
 *
 *  This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 *  without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 *  See the GNU General Public License for more details.
 *
 *  The Original Code was created by Simon Carter (s1cart3r@gmail.com)
 *
 *  Copyright (c) 2018 - 2019 Simon Carter.  All Rights Reserved.
 *
 *  Product:  UserAccount.Plugin
 *  
 *  File: AccountController.Invoices.cs
 *
 *  Purpose:  Manages Invoices
 *
 *  Date        Name                Reason
 *  04/01/2019  Simon Carter        Initially Created
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using Middleware.Accounts.Invoices;

using UserAccount.Plugin.Models;

namespace UserAccount.Plugin.Controllers
{
    public partial class AccountController
    {
        #region Public Action Methods

        [HttpGet]
        public ActionResult Invoices()
        {
            InvoicesViewModel model = new InvoicesViewModel(_accountProvider.InvoicesGet(UserId()));

            return View(model);
        }

        [HttpGet]
        public ActionResult InvoiceView(int id)
        {
            Invoice invoice = _accountProvider.InvoicesGet(UserId()).Where(o => o.Id == id).FirstOrDefault();

            if (invoice == null)
                return RedirectToAction(nameof(Index));

            InvoiceViewModel model = new InvoiceViewModel(invoice);

            return View(model);
        }

        #endregion Public Action Methods
    }
}
