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
 *  Product:  PluginMiddleware
 *  
 *  File: DeliveryAddress.cs
 *
 *  Purpose:  Delivery Address
 *
 *  Date        Name                Reason
 *  16/12/2018  Simon Carter        Initially Created
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

namespace Middleware.Accounts
{
    public sealed class Marketing
    {
        #region Constructors

        public Marketing()
        {

        }

        public Marketing(bool email, bool telephone, bool sms, bool postal)
        {
            EmailOffers = email;
            TelephoneOffers = telephone;
            SMSOffers = sms;
            PostalOffers = postal;
        }

        #endregion Constructors

        #region Properties

        public bool EmailOffers { get; set; }

        public bool TelephoneOffers { get; set; }

        public bool SMSOffers { get; set; }

        public bool PostalOffers { get; set; }

        #endregion Properties
    }
}
