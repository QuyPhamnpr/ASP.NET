using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebBanSmartphones.Models;

namespace WebBanSmartphones.Context
{
    [MetadataType(typeof(UserMasterData))]
    public partial class User
    {

    }
    [MetadataType(typeof(ProductMasterData))]
    public partial class Product_2119110227
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }
}