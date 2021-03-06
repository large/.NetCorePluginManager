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
 *  File: MarketingPreferencesViewModel.cs
 *
 *  Purpose:  
 *
 *  Date        Name                Reason
 *  30/12/2018  Simon Carter        Initially Created
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
using System.ComponentModel.DataAnnotations;

namespace UserAccount.Plugin.Models
{
    public sealed class MarketingPreferencesViewModel
    {
        [Display(Name = "Accept offers via Email")]
        public bool EmailOffers { get; set; }

        [Display(Name = "Accept offers via Telephone")]
        public bool TelephoneOffers { get; set; }

        [Display(Name = "Accept offers via SMS")]
        public bool SMSOffers { get; set; }

        [Display(Name = "Accept offers via Post")]
        public bool PostalOffers { get; set; }

        public bool ShowEmail { get; set; }

        public bool ShowTelephone { get; set; }

        public bool ShowSMS { get; set; }

        public bool ShowPostal { get; set; }
    }
}
