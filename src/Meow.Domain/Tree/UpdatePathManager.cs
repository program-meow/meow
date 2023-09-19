﻿using Meow.Data.Extension;
using Meow.Error;
using Meow.Exception;
using Meow.Extension;

namespace Meow.Domain.Tree;

/// <summary>
/// 树形路径更新服务
/// </summary>
public class UpdatePathManager<TEntity, TKey, TParentId>
    where TEntity : class, ITreeEntity<TEntity , TKey , TParentId> {
    /// <summary>
    /// 仓储
    /// </summary>
    private readonly ITreeRepository<TEntity , TKey , TParentId> _repository;

    /// <summary>
    /// 初始化树型路径更新服务
    /// </summary>
    /// <param name="repository">仓储</param>
    public UpdatePathManager( ITreeRepository<TEntity , TKey , TParentId> repository ) {
        _repository = repository;
    }

    /// <summary>
    /// 更新实体及所有下级节点路径
    /// </summary>
    /// <param name="entity">实体</param>
    public async Task UpdatePathAsync( TEntity entity ) {
        entity.CheckNull( nameof( entity ) );
        if( entity.ParentId.Equals( entity.Id ) )
            throw new Warning( ErrorMessageKey.NotSupportMoveToChildren );
        TEntity old = await _repository.NoTracking().FindByIdAsync( entity.Id );
        if( old == null )
            return;
        if( entity.ParentId.Equals( old.ParentId ) )
            return;
        List<TEntity> children = await _repository.GetAllChildrenAsync( entity );
        if( children.Exists( t => t.Id.Equals( entity.ParentId ) ) )
            throw new Warning( ErrorMessageKey.NotSupportMoveToChildren );
        TEntity parent = await _repository.FindByIdAsync( entity.ParentId );
        entity.InitPath( parent );
        UpdateChildrenPath( entity , children );
        await _repository.UpdateAsync( children );
    }

    /// <summary>
    /// 修改路径
    /// </summary>
    private void UpdateChildrenPath( TEntity parent , List<TEntity> children ) {
        if( parent == null || children == null )
            return;
        List<TEntity> list = children.Where( t => t.ParentId.Equals( parent.Id ) ).ToList();
        foreach( TEntity child in list ) {
            child.InitPath( parent );
            UpdateChildrenPath( child , children );
        }
    }
}