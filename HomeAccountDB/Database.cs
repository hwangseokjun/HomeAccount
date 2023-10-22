using HomeAccountDB.Controllers;
using HomeAccountDB.Repository;
using HomeAccountDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB
{
    public class Database
    {
        // Controller
        private readonly ExpenseController _expenseController;
        private readonly ExpenseCategoryController _expenseCategoryController;
        private readonly ExpenseMethodController _expenseMethodController;
        private readonly ExpenseSourceController _expenseSourceController;

        private readonly IncomeController _incomeController;
        private readonly IncomeCategoryController _incomeCategoryController;
        private readonly IncomeMethodController _incomeMethodController;
        private readonly IncomeSourceController _incomeSourceController;

        // Service
        private readonly ExpenseService _expenseService;
        private readonly ExpenseCategoryService _expenseCategoryService;
        private readonly ExpenseMethodService _expenseMethodService;
        private readonly ExpenseSourceService _expenseSourceService;

        private readonly IncomeService _incomeService;
        private readonly IncomeCategoryService _incomeCategoryService;
        private readonly IncomeMethodService _incomeMethodService;
        private readonly IncomeSourceService _incomeSourceService;

        // Repository
        private readonly IExpenseRepository _expenseRepository;
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;
        private readonly IExpenseMethodRepository _expenseMethodRepository;
        private readonly IExpenseSourceRepository _expenseSourceRepository;

        private readonly IIncomeRepository _incomeRepository;
        private readonly IIncomeCategoryRepository _incomeCategoryRepository;
        private readonly IIncomeMethodRepository _incomeMethodRepository;
        private readonly IIncomeSourceRepository _incomeSourceRepository;

        public ExpenseController Expense => _expenseController;
        public ExpenseCategoryController ExpenseCategory => _expenseCategoryController;
        public ExpenseMethodController ExpenseMethod => _expenseMethodController;
        public ExpenseSourceController ExpenseSource => _expenseSourceController;

        public IncomeController Income => _incomeController;
        public IncomeCategoryController IncomeCategory => _incomeCategoryController;
        public IncomeMethodController IncomeMethod => _incomeMethodController;
        public IncomeSourceController IncomeSource => _incomeSourceController;


        public Database()
        {
            // Initialize Repositories
            _expenseRepository = new ExpenseRepository();
            _expenseCategoryRepository = new ExpenseCategoryRepository();
            _expenseMethodRepository = new ExpenseMethodRepository();
            _expenseSourceRepository = new ExpenseSourceRepository();

            _incomeRepository = new IncomeRepository();
            _incomeCategoryRepository = new IncomeCategoryRepository();
            _incomeMethodRepository = new IncomeMethodRepository();
            _incomeSourceRepository = new IncomeSourceRepository();

            // Initialize Services
            _expenseService = new ExpenseService(_expenseRepository);
            _expenseCategoryService = new ExpenseCategoryService(_expenseCategoryRepository);
            _expenseMethodService = new ExpenseMethodService(_expenseMethodRepository);
            _expenseSourceService = new ExpenseSourceService(_expenseSourceRepository);

            _incomeService = new IncomeService(_incomeRepository);
            _incomeCategoryService = new IncomeCategoryService(_incomeCategoryRepository);
            _incomeMethodService = new IncomeMethodService(_incomeMethodRepository);
            _incomeSourceService = new IncomeSourceService(_incomeSourceRepository);

            // Initialize Controllers
            _expenseController = new ExpenseController(_expenseService);
            _expenseCategoryController = new ExpenseCategoryController(_expenseCategoryService);
            _expenseMethodController = new ExpenseMethodController(_expenseMethodService);
            _expenseSourceController = new ExpenseSourceController(_expenseSourceService);

            _incomeController = new IncomeController(_incomeService);
            _incomeCategoryController = new IncomeCategoryController(_incomeCategoryService);
            _incomeMethodController = new IncomeMethodController(_incomeMethodService);
            _incomeSourceController = new IncomeSourceController(_incomeSourceService);
        }
    }
}
