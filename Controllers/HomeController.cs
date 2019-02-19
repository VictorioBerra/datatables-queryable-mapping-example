using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DataTables.Queryable;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using System.Linq.Expressions;
using AutoMapper;
using static DataTables.Queryable.DataTablesAjaxPostModel;

namespace mvctester.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpPost("")]
        public ActionResult<DataTablesAjaxPostModel> Test(
            DataTablesAjaxPostModelTyped<CatViewModel, Cat> viewmodel,
            string param1)
        {
            Mapper.Reset();
            Mapper.Initialize(config => {

                config
                    .CreateMap<DataTablesAjaxPostModelTyped<CatViewModel, Cat>, DataTablesAjaxPostModel>()
                        .ForMember(dest => dest.Columns,
                            opt => opt.ConvertUsing(new DataTablesAjaxPostModelColumnExpressionStringConverter<CatViewModel, Cat>(new Dictionary<Expression<Func<CatViewModel, object>>, Expression<Func<Cat, object>>>()
                            {
                        {catVm => catVm.Name, catEntity => catEntity.CatName},
                        {catVm => catVm.Breed.Name, catEntity => catEntity.CatBreed.BreedName}
                            })));

            });

            var newViewModel = Mapper.Map<DataTablesAjaxPostModel>(viewmodel);

            return newViewModel;
        }

    }

    /// <summary>
    /// Class to provide some type context for automapping the columns on <see cref="DataTablesAjaxPostModel"/> (type information specifically).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataTablesAjaxPostModelTyped<TSource, TDest> : DataTablesAjaxPostModel
    {
        // Intentionally left empty. See summary.
    }

    /// <summary>
    /// Given a list of strings, replace them from a provided mapping definition.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataTablesAjaxPostModelColumnExpressionStringConverter<TSource, TDest> : IValueConverter<List<ColumnData>, List<ColumnData>>
    {
        private Dictionary<string, string> _expressionAsStringMap { get; } = new Dictionary<string, string>();

        public DataTablesAjaxPostModelColumnExpressionStringConverter(Dictionary<Expression<Func<TSource, object>>, Expression<Func<TDest, object>>> expressionMap)
        {
            foreach (KeyValuePair<Expression<Func<TSource, object>>, Expression<Func<TDest, object>>> entry in expressionMap)
            {
                var expressionSrcStr = ExpressionHelper.GetExpressionText(entry.Key);
                var expressionDestStr = ExpressionHelper.GetExpressionText(entry.Value);
                _expressionAsStringMap.Add(expressionSrcStr, expressionDestStr);
            }

        }

        public List<ColumnData> Convert(List<ColumnData> sourceMember, ResolutionContext context)
        {
            // Todo, support for column.Name?
            foreach (var column in sourceMember)
            {
                var columnData = column.Data;
                var foundMap = _expressionAsStringMap.TryGetValue(columnData, out string mapValue);
                if (!string.IsNullOrEmpty(mapValue))
                {
                    // Mutate sourceMember
                    column.Data = mapValue;
                }
            }

            return sourceMember;
        }
    }

    // Some fake VMs and EF Entities
    public class CatViewModel
    {
        public string Name { get; set; }
        public BreedViewModel Breed { get; set; }
    }
    public class BreedViewModel
    {
        public string Name { get; set; }
    }
    public class Cat
    {
        public string CatName { get; set; }
        public Breed CatBreed { get; set; }
    }
    public class Breed
    {
        public string BreedName { get; set; }
    }

}
