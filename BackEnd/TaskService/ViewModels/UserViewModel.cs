﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.ViewModels
{
    public class UserViewModel
    {
        public Guid ID { get; set; }
        public List<RoleViewModel> roles { get; set; }
    }
}
