﻿using System;
using Meow.Extension.Helper;
using System.Threading.Tasks;
using Meow.Sample.Domain.Model;
using System.Collections.Generic;
using Meow.Sample.Data.UnitOfWork;
using Meow.Sample.Domain.Repository;
using Meow.Sample.Service.Abstractions.Systems;

namespace Meow.Sample.Service.Implements.Systems
{
    /// <summary>
    /// 应用程序服务
    /// </summary>
    public class ApplicationService : IApplicationService
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        protected ISampleUnitOfWork UnitOfWork { get; set; }
        /// <summary>
        /// 应用程序仓储
        /// </summary>
        protected IApplicationRepository ApplicationRepository { get; set; }


        /// <summary>
        /// 初始化应用程序服务
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="applicationRepository">应用程序仓储</param>
        public ApplicationService(
            ISampleUnitOfWork unitOfWork
            , IApplicationRepository applicationRepository
             )
        {
            UnitOfWork = unitOfWork;
            ApplicationRepository = applicationRepository;
        }

        /// <summary>
        /// 添加
        /// </summary>
        public async Task AddAsync()
        {
            var entity = new Application
            {
                Code = "B",
                Name = DateTime.Now.SafeString(),
            };
            entity.Init();
            await ApplicationRepository.AddAsync(entity);
            await UnitOfWork.CommitAsync();
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        public async Task<List<Application>> GetAllAsync()
        {
            var entities = await ApplicationRepository.FindAllAsync();
            return entities;
        }

        /// <summary>
        /// 删除所有
        /// </summary>
        public async Task DeleteAllAsync()
        {
            var entities = await ApplicationRepository.FindAllAsync();
            await ApplicationRepository.RemoveAsync(entities);
            await UnitOfWork.CommitAsync();
        }
    }
}