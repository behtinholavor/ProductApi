using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace product.stock.api
{
    public static class StockFactory
    {
        public static IConfiguration _configuration { get; set; }

        public static ProductVM ToModelVM(this Product model)
        {
            if (model == null)
                return null;

            var viewModel = new ProductVM()
            {
                Id = model.Id,
                Description = model.Description,
                Price = model.Price,
                Quantity = model.Quantity,
                Unit = model.Unit,
                Category = model.Category,
                Inserted = model.Inserted,
                Modified = model.Modified
            };

            return viewModel;
        }

        public static List<ProductVM> ToListModelVM(this List<Product> models)
        {
            List<ProductVM> views = new List<ProductVM>();
            if (models == null)
                return views;

            foreach (var model in models)
                views.Add(model.ToModelVM());
                        
            return views;
        }

        public static Product ToVMModel(this ProductVM viewModel)
        {
            if (viewModel == null)
                return null;

            var model = new Product()
            {
                Id = viewModel.Id,
                Description = viewModel.Description,
                Price = viewModel.Price,
                Quantity = viewModel.Quantity,
                Unit = viewModel.Unit,
                Category = viewModel.Category,
                Inserted = viewModel.Inserted,
                Modified = viewModel.Modified                
            };

            return model;
        }

        public static List<Product> ToListVMModel(this List<ProductVM> views)
        {
            List<Product> models = new List<Product>();
            if (views == null)
                return models;
            
            foreach (var view in views)
                models.Add(view.ToVMModel());

            return models;
        }

        public static UserVM ToModelVM(this User model)
        {
            if (model == null)
                return null;

            var viewModel = new UserVM()
            {
                Id = model.Id,
                Login = model.Login,
                Pass = model.Pass,
                Token = model.Token,
                Validity = model.Validity
            };

            return viewModel;
        }

        public static User ToVMModel(this UserVM viewModel)
        {
            if (viewModel == null)
                return null;

            var model = new User()
            {
                Id = viewModel.Id,
                Login = viewModel.Login,
                Pass = viewModel.Pass,
                Token = viewModel.Token,
                Validity = viewModel.Validity != null ? viewModel.Validity : DateTime.Now.AddHours(24)
            };

            return model;
        }

        public static string BuildToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var param = _configuration.GetSection("Security:Token").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(param));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            JwtSecurityToken security = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: user.Validity,
               signingCredentials: creds);

            var token = new JwtSecurityTokenHandler().WriteToken(security);

            return token;
        }

        public static AuthVM ToAuthVM(this UserVM user)
        {
            if (user == null)
                return null;

            var auth = new AuthVM()
            {
                Token = user.Token,
                Validity = user.Validity
            };

            return auth;
        }



    }
}
