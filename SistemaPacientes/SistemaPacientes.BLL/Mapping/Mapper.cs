namespace SistemaPacientes.BLL.Mapping;

/// <summary>Copia las propiedades con el mismo nombre y tipo entre Entidad y DTO.</summary>
public static class Mapper
{
    public static TDest Map<TSource, TDest>(TSource origen, TDest? destino = null)
        where TSource : class
        where TDest : class, new()
    {
        destino ??= new TDest();

        foreach (var pd in typeof(TDest).GetProperties().Where(p => p.CanWrite))
        {
            var po = typeof(TSource).GetProperty(pd.Name);
            if (po is null || !po.CanRead || po.PropertyType != pd.PropertyType) continue;
            pd.SetValue(destino, po.GetValue(origen));
        }
        return destino;
    }
}
