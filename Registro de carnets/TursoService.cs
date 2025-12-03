// using LibSql.Client;
// using System.Collections.Generic;
// using System.Threading.Tasks;
//
// public class TursoService
// {
//     private readonly LibSqlClient _client;
//
//     public TursoService()
//     {
//         // Obtener configuración
//         var settings = new TursoSettings();
//         App.Configuration.GetSection("Turso").Bind(settings);
//
//         // Crear cliente
//         var options = new LibSqlOptions
//         {
//             Url = settings.Url,
//             AuthToken = settings.AuthToken
//         };
//
//         _client = new LibSqlClient(options);
//     }
//
//     // Consulta SELECT que retorna múltiples registros
//     public async Task<List<T>> QueryAsync<T>(string sql) where T : new()
//     {
//         var result = await _client.ExecuteAsync(sql);
//         return MapToList<T>(result);
//     }
//
//     // Consulta SELECT con parámetros
//     public async Task<List<T>> QueryAsync<T>(string sql, Dictionary<string, object> parameters) where T : new()
//     {
//         var result = await _client.ExecuteAsync(sql, parameters);
//         return MapToList<T>(result);
//     }
//
//     // Consulta que retorna un solo registro
//     public async Task<T> QuerySingleAsync<T>(string sql) where T : new()
//     {
//         var result = await _client.ExecuteAsync(sql);
//         var list = MapToList<T>(result);
//         return list.FirstOrDefault();
//     }
//
//     // INSERT, UPDATE, DELETE
//     public async Task<int> ExecuteAsync(string sql)
//     {
//         var result = await _client.ExecuteAsync(sql);
//         return result.RowsAffected;
//     }
//
//     // INSERT, UPDATE, DELETE con parámetros
//     public async Task<int> ExecuteAsync(string sql, Dictionary<string, object> parameters)
//     {
//         var result = await _client.ExecuteAsync(sql, parameters);
//         return result.RowsAffected;
//     }
//
//     // Mapear resultados a objetos
//     private List<T> MapToList<T>(LibSqlResult result) where T : new()
//     {
//         var list = new List<T>();
//         var properties = typeof(T).GetProperties();
//
//         foreach (var row in result.Rows)
//         {
//             var item = new T();
//             
//             for (int i = 0; i < result.Columns.Count; i++)
//             {
//                 var columnName = result.Columns[i];
//                 var property = properties.FirstOrDefault(p => 
//                     p.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));
//
//                 if (property != null && row[i] != null)
//                 {
//                     property.SetValue(item, Convert.ChangeType(row[i], property.PropertyType));
//                 }
//             }
//             
//             list.Add(item);
//         }
//
//         return list;
//     }
// }