using Catridges.DAL.Interfaces;
using Catridges.Domain.Entity;
using Catridges.Domain.Enum;
using Catridges.Domain.Response;
using Catridges.Service.Interfases;

namespace Catridges.Service.Implementations;

public class SubDivisionService : ISubdivisionService
{
    private readonly ISubdivisionRepository _subdivisionRepository;

    public SubDivisionService(ISubdivisionRepository subdivisionRepository)
    {
        _subdivisionRepository = subdivisionRepository;
    }

    public async Task<BaseResponse<bool>> Create(Subdivision? subdivision)
    {
        var baseResponse = new BaseResponse<bool>();
        try
        {
            if (subdivision != null)
            {
                var subdivisions = await _subdivisionRepository.ReadAll();
                foreach (var item in subdivisions)
                {
                    if (subdivision.Name == item.Name)
                    {
                        baseResponse.StatusCode = StatusCode.NoOk;
                        return baseResponse;
                    }
                }
                await _subdivisionRepository.Create(subdivision);
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

    public async Task<BaseResponse<Subdivision>> Read(int id)
    {
        var baseResponse = new BaseResponse<Subdivision>();
        try
        {
            var subdivision = await _subdivisionRepository.Read(id);
            if (subdivision != null)
            {
                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = subdivision;
                return baseResponse;
            }

            baseResponse.StatusCode = StatusCode.NoOk;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<Subdivision>()
            {
                Description = $"[Read] : {e.Message}"
            };
        }
    }

    public async Task<BaseResponse<bool>> Update(int id, Subdivision subdivisionnew)
    {
        var baseResponse = new BaseResponse<bool>();
        try
        {
            var subdivisions = await _subdivisionRepository.ReadAll();
            var subdivision = await _subdivisionRepository.Read(id);
            if (subdivision != null)
            {
                foreach (var item in subdivisions)
                {
                    if (item.Name == subdivisionnew.Name)
                    {
                        baseResponse.StatusCode = StatusCode.NoOk;
                        return baseResponse;
                    }
                }
                subdivision.Name = subdivisionnew.Name;
                await _subdivisionRepository.Update(subdivision.Id, subdivision);
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

    public async Task<BaseResponse<List<Subdivision>>> ReadAll()
    {
        var baseResponse = new BaseResponse<List<Subdivision>>();
        try
        {
            var subdivisions = await _subdivisionRepository.ReadAll();
            if (subdivisions.Count > 0)
            {
                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = subdivisions;
                return baseResponse;
            }
            baseResponse.StatusCode = StatusCode.NoOk;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<List<Subdivision>>()
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
            var subdivision = await _subdivisionRepository.Read(id);
            if (subdivision != null)
            {
                await _subdivisionRepository.Delete(subdivision);
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