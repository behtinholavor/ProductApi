using System;
using System.Collections.Generic;

namespace product.stock.api
{
    public class StockService
    {
        private readonly IStockRepository _repository;

        public StockService(){ }

        public StockService(IStockRepository repository)
        {
            _repository = repository;
        }
            
        public List<ProductVM> List()
        {            
            List<Product> models = _repository.List().GetAwaiter().GetResult();
            return models.ToListModelVM();
        }

        public ProductVM Select(long id)
        {
            Product model = _repository.Select(id);
            return model.ToModelVM();            
        }

        public ProductVM Insert(ProductVM viewModel)
        {
            Product model = viewModel.ToVMModel();
            model.Inserted = DateTime.Now;
            _repository.Insert(model);
            return model.ToModelVM();
        }

        public ProductVM Update(ProductVM viewModel)
        {
            Product model = viewModel.ToVMModel();
            model.Modified = DateTime.Now;
            _repository.Update(model);
            return model.ToModelVM();
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public UserVM Insert(UserVM viewModel)
        {
            User model = viewModel.ToVMModel();
            UserVM view = _repository.Insert(model);            
            return view;
        }

        public UserVM Login(UserVM viewModel)
        {
            User model = viewModel.ToVMModel();
            UserVM view = _repository.Login(model);
            return view;
        }

    }
}
