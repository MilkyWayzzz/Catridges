using Catridges.DAL.Interfaces;
using Catridges.Domain.Entity;
using Catridges.Domain.Enum;
using Catridges.Domain.Response;
using Catridges.Service.Interfases;

namespace Catridges.Service.Implementations;

public class CatridgeService : ICatridgeService
{
    private readonly ICatridgeRepository _catridgeRepository;

    public CatridgeService(ICatridgeRepository catridgeRepository)
    {
        _catridgeRepository = catridgeRepository;
    }

    public async Task<BaseResponse<bool>> Create(Catridge? catridge)
    {
        var baseResponse = new BaseResponse<bool>();
        try
        {
            if (catridge != null)
            {
                var catridges = await _catridgeRepository.ReadAll();
                foreach (var item in catridges)
                {
                    if (catridge.Model == item.Model)
                    {
                        baseResponse.StatusCode = StatusCode.NoOk;
                        return baseResponse;
                    }
                }
                await _catridgeRepository.Create(catridge);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }

            baseResponse.StatusCode = StatusCode.NoOk;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<bool>()
            {
                Description = $"[Create] : {e.Message}"
            };
        }
    }

    public async Task<BaseResponse<Catridge>> Read(int id)
    {
        var baseResponse = new BaseResponse<Catridge>();
        try
        {
            var catridge = await _catridgeRepository.Read(id);
            if (catridge != null)
            {
                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = catridge;
                return baseResponse;
            }

            baseResponse.StatusCode = StatusCode.NoOk;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<Catridge>()
            {
                Description = $"[Read] : {e.Message}"
            };
        }
    }

    public async Task<BaseResponse<bool>> Update(int id, Catridge catridgenew)
    {
        var baseResponse = new BaseResponse<bool>();
        try
        {
            var catridges = await _catridgeRepository.ReadAll();
            var catridge = await _catridgeRepository.Read(id);
            if (catridge != null)
            {
                foreach (var item in catridges)
                {
                    if (item.Model == catridgenew.Model)
                    {
                        baseResponse.StatusCode = StatusCode.NoOk;
                        return baseResponse;
                    }
                }
                catridge.Model = catridgenew.Model;
                await _catridgeRepository.Update(catridge.Id,catridge);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }

            baseResponse.StatusCode = StatusCode.NoOk;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<bool>()
            {
                Description = $"[ReadAll] : {e.Message}"
            };
        }
    }

    public async Task<BaseResponse<List<Catridge>>> ReadAll()
    {
        var baseResponse = new BaseResponse<List<Catridge>>();
        try
        {
            var catridges = await _catridgeRepository.ReadAll();
            if (catridges.Count > 0)
            {
                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = catridges;
                return baseResponse;
            }
            baseResponse.StatusCode = StatusCode.NoOk;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<List<Catridge>>()
            {
                Description = $"[ReadAll] : {e.Message}"
            };
        }
    }

    public async Task<BaseResponse<bool>> Delete(int id)
    {
        var baseResponse = new BaseResponse<bool>();
        try
        {
            var catridge = await _catridgeRepository.Read(id);
            if (catridge != null)
            {
                await _catridgeRepository.Delete(catridge);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }

            baseResponse.StatusCode = StatusCode.NoOk;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<bool>()
            {
                Description = $"[Delete] : {e.Message}"
            };
        }
    }
}