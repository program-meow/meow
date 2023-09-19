﻿using Meow.Data.Sql.Builder.Param;

namespace Meow.Data.Sql.Builder;

/// <summary>
/// Sql生成器结果
/// </summary>
public class SqlBuilderResult {
    /// <summary>
    /// Sql生成器结果
    /// </summary>
    private readonly string _sql;
    /// <summary>
    /// Sql参数列表
    /// </summary>
    private readonly List<SqlParam> _sqlParams;
    /// <summary>
    /// 参数字面值解析器
    /// </summary>
    private readonly IParamLiteralsResolver _resolver;
    /// <summary>
    /// Sql参数管理器
    /// </summary>
    private IParameterManager _parameterManager;

    /// <summary>
    /// 初始化Sql生成器结果
    /// </summary>
    /// <param name="sql">Sql</param>
    /// <param name="sqlParams">Sql参数列表</param>
    /// <param name="resolver">参数字面值解析器</param>
    /// <param name="parameterManager">Sql参数管理器</param>
    public SqlBuilderResult( string sql , List<SqlParam> sqlParams , IParamLiteralsResolver resolver , IParameterManager parameterManager ) {
        _sql = sql;
        _sqlParams = sqlParams ?? new List<SqlParam>();
        _resolver = resolver;
        _parameterManager = parameterManager;
    }

    /// <summary>
    /// 获取Sql语句
    /// </summary>
    public string GetSql() {
        return _sql;
    }

    /// <summary>
    /// 获取Sql参数列表
    /// </summary>
    public List<SqlParam> GetParams() {
        return _sqlParams;
    }

    /// <summary>
    /// 获取Sql参数值
    /// </summary>
    /// <typeparam name="T">参数值类型</typeparam>
    /// <param name="name">参数名</param>
    public T GetParam<T>( string name ) {
        name = _parameterManager.NormalizeName( name );
        SqlParam result = _sqlParams.Find( t => t.Name?.ToUpperInvariant() == name?.ToUpperInvariant() );
        if( result == null )
            return default;
        return Meow.Helper.Convert.To<T>( result.Value );
    }

    /// <summary>
    /// 获取调试Sql语句
    /// </summary>
    public virtual string GetDebugSql() {
        string sql = GetSql();
        List<SqlParam> parameters = GetParams();
        foreach( SqlParam parameter in parameters )
            sql = Meow.Helper.Regex.Replace( sql , $@"{parameter.Name}\b" , _resolver.GetParamLiterals( parameter.Value ) );
        return sql;
    }
}