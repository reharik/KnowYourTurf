//using System.Linq;
//using KnowYourTurf.Core.Domain;
//using KnowYourTurf.Core.Html.Grid;
//using KnowYourTurf.Core.Localization;
//using StructureMap;

//namespace KnowYourTurf.Core.Services
//{
//    public interface IGridHandlerService
//    {
//        GridDefinition BuildGridDefinition<ENTITY>(string url,
//                                            string gridType,
//                                            StringToken title = null) where ENTITY : DomainEntity;

//        GridItemsViewModel GridItemsViewModel<ENTITY>(PageSortFilter pageSortFilter,
//                                                      IQueryable<ENTITY> items,
//                                                      string gridType) where ENTITY : DomainEntity;
//    }

//    public class GridHandlerService : IGridHandlerService
//    {
//        private readonly IRepository _aggregateRootRepo;
//        private readonly ISessionContext _sessionContext;
//        private readonly IContainer _container;

//        public GridHandlerService(IRepository repository, ISessionContext sessionContext, IContainer container)
//        {
//            _repository = repository;
//            _sessionContext = sessionContext;
//            _container = container;
//        }

//        public GridDefinition BuildGridDefinition<ENTITY>(string url,
//                                                string gridType,
//                                                StringToken title = null) where ENTITY : DomainEntity
//        {
//            var userId = _sessionContext.GetUserId();
//            var user = _repository.Find<User>(userId);
//            //change this to grab the interface for the grid named.
//            var grid = gridType.IsEmpty()
//                           ? ObjectFactory.Container.GetInstance<IGrid<ENTITY>>()
//                           : ObjectFactory.Container.GetInstance<IGrid<ENTITY>>(gridType);
//            var builtGrid = BuiltGrid(grid);
//            return new GridDefinition
//                       {
//                           Url = url,
//                           Title = title.ToString(),
//                           Columns = builtGrid.GetGridColumns(user)
//                       };
//        }

//        public GridItemsViewModel GridItemsViewModel<ENTITY>(PageSortFilter pageSortFilter,
//                                                            IQueryable<ENTITY> items,
//                                                            string gridType = null) where ENTITY:DomainEntity
//        {
//            var userId = _sessionContext.GetUserId();
//            var user = _repository.Find<User>(userId);
//            var grid = gridType.IsEmpty()
//                           ? ObjectFactory.Container.GetInstance<IGrid<ENTITY>>()
//                           : ObjectFactory.Container.GetInstance<IGrid<ENTITY>>(gridType);
//            var buildGrid = BuiltGrid(grid);
//            var pager = new Pager<ENTITY>();
//            var pageAndSort = pager.PageAndSort(items, pageSortFilter);
//            var model = new GridItemsViewModel
//            {
//                items = buildGrid.GetGridRows(pageAndSort.Items, user),
//                page = pageAndSort.Page,
//                records = pageAndSort.TotalRows,
//                total = pageAndSort.TotalPages
//            };
//            return model;
//        }

//        private IGrid<ENTITY> BuiltGrid<ENTITY>(IGrid<ENTITY> grid) where ENTITY : DomainEntity
//        {
//            IGrid<ENTITY> builtGrid;
//            builtGrid = grid.BuildGrid();
//            return builtGrid;
//        }
//    }
//}