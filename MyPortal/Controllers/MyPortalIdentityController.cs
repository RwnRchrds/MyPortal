﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPortal.Interfaces;
using MyPortal.Models;
using MyPortal.Models.Database;

namespace MyPortal.Controllers
{
    public class MyPortalIdentityController : MyPortalController
    {
        protected readonly IdentityContext _identity;
        protected readonly UserManager<ApplicationUser, string> _userManager;
        protected readonly UserStore<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole,
            IdentityUserClaim> _userStore;

        protected readonly RoleManager<ApplicationRole, string> _roleManager;
        protected readonly RoleStore<ApplicationRole> _roleStore;

        public MyPortalIdentityController()
        {
            _identity = new IdentityContext();
            _userStore = new UserStore<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole,
                IdentityUserClaim>(_identity);
            _userManager = new UserManager<ApplicationUser, string>(_userStore);
            _roleStore = new RoleStore<ApplicationRole>(_identity);
            _roleManager = new RoleManager<ApplicationRole, string>(_roleStore);
        }

        public MyPortalIdentityController(IUnitOfWork unitOfWork, IdentityContext identity) : base()
        {
            _identity = new IdentityContext();
            _userStore = new UserStore<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole,
                IdentityUserClaim>(_identity);
            _userManager = new UserManager<ApplicationUser, string>(_userStore);
            _roleStore = new RoleStore<ApplicationRole>(_identity);
            _roleManager = new RoleManager<ApplicationRole, string>(_roleStore);
        }

        protected override void Dispose(bool disposing)
        {
            _roleManager.Dispose();
            _roleStore.Dispose();
            _userManager.Dispose();
            _userStore.Dispose();
            _identity.Dispose();
        }
    }
}