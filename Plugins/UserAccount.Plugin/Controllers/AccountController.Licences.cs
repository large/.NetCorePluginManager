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
 *  File: AccountController.Licences.cs
 *
 *  Purpose:  Manages user downloads
 *
 *  Date        Name                Reason
 *  06/01/2019  Simon Carter        Initially Created
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using UserAccount.Plugin.Models;

using Middleware.Accounts.Licences;

namespace UserAccount.Plugin.Controllers
{
    public partial class AccountController
    {
        #region Public Action Methods

        public IActionResult Licences()
        {
            List<ViewLicenceViewModel> licences = new List<ViewLicenceViewModel>();

            foreach (Licence licence in _licenceProvider.LicencesGet(UserId()))
            {
                licences.Add(new ViewLicenceViewModel(licence.Id, licence.DomainName, licence.LicenceType.Description,
                    Shared.Utilities.DateWithin(licence.ExpireDate, licence.StartDate, DateTime.Now) && licence.IsValid,
                    licence.IsTrial, licence.ExpireDate, licence.UpdateCount, licence.EncryptedLicence));
            }

            LicenceViewModel model = new LicenceViewModel(licences, GrowlGet());

            return View(model);
        }

        [HttpGet]
        public IActionResult LicenceView(int id)
        {
            ViewLicenceViewModel model = null;
            Licence licence = _licenceProvider.LicencesGet(UserId()).Where(l => l.Id == id).FirstOrDefault();

            if (licence != null)
            {
                model = new ViewLicenceViewModel(licence.Id, licence.DomainName, licence.LicenceType.Description,
                    Shared.Utilities.DateWithin(licence.ExpireDate, licence.StartDate, DateTime.Now) && licence.IsValid,
                    licence.IsTrial, licence.ExpireDate, licence.UpdateCount, licence.EncryptedLicence);
            }

            if (model == null)
                RedirectToAction(nameof(Licences));

            return View(model);
        }

        [HttpGet]
        public IActionResult LicenceCreate()
        {
            return View(new CreateLicenceViewModel());
        }

        [HttpPost]
        public IActionResult LicenceCreate(CreateLicenceViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            LicenceType licenceType = _licenceProvider.LicenceTypesGet().Where(l => l.Id == model.LicenceType).FirstOrDefault();

            if (licenceType == null)
                ModelState.AddModelError(String.Empty, "Invalid Licence Type");

            if (ModelState.IsValid)
            {
                switch (_licenceProvider.LicenceTrialCreate(UserId(), licenceType))
                {
                    case Middleware.LicenceCreate.Existing:
                        GrowlAdd($"You already have a trial licence for {licenceType.Description}");
                        break;

                    case Middleware.LicenceCreate.Failed:
                        GrowlAdd("Failed to create a trial licence");
                        break;

                    case Middleware.LicenceCreate.Success:
                        GrowlAdd("Trial licence succesfully created");
                        break;
                }

                return RedirectToAction(nameof(Licences));
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult LicenceUpdateDomain(ViewLicenceViewModel model)
        {
            Licence licence = _licenceProvider.LicencesGet(UserId()).Where(l => l.Id == model.Id).FirstOrDefault();

            if (!Shared.Utilities.ValidateIPAddress(model.Domain))
            {
                ModelState.AddModelError(nameof(model.Domain), "Invalid ip address");
                return View(nameof(LicenceView), model);
            }

            if (licence != null )
            {
                if (_licenceProvider.LicenceUpdateDomain(UserId(), licence, model.Domain))
                {
                    GrowlAdd("Licence updated");
                    return RedirectToAction(nameof(Licences));
                }
            }

            GrowlAdd("Failed to update licence");
            return RedirectToAction(nameof(Licences));
        }

        public IActionResult LicenceSendEmail(int id)
        {
            Licence licence = _licenceProvider.LicencesGet(UserId()).Where(l => l.Id == id).FirstOrDefault();

            if (licence != null)
            {
                if (_licenceProvider.LicenceSendEmail(UserId(), id))
                {
                    GrowlAdd("Email Sent");
                    return RedirectToAction(nameof(Licences));
                }
            }

            GrowlAdd("Failed to send email");
            return RedirectToAction(nameof(Licences));
        }

        #endregion Public Action Methods
    }
}
