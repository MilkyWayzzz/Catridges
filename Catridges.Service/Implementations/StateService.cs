using Catridges.DAL.Interfaces;
using Catridges.Domain.Entity;
using Catridges.Domain.Enum;
using Catridges.Domain.Response;
using Catridges.Service.Interfases;

namespace Catridges.Service.Implementations;

public class StateService : IStateServise
{
    private readonly IStateRepository _stateRepository;

    public StateService(IStateRepository stateRepository)
    {
        _stateRepository = stateRepository;
    }

    public async Task<BaseResponse<bool>> Create(State? state)
    {
        var baseResponse = new BaseResponse<bool>();
        try
        {
            if (state != null)
            {
                var states = await _stateRepository.ReadAll();
                foreach (var item in states)
                {
                    if (state.Name == item.Name)
                    {
                        baseResponse.StatusCode = StatusCode.NoOk;
                        return baseResponse;
                    }
                }
                await _stateRepository.Create(state);
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

    public async Task<BaseResponse<State>> Read(int id)
    {
        var baseResponse = new BaseResponse<State>();
        try
        {
            var state = await _stateRepository.Read(id);
            if (state != null)
            {
                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = state;
                return baseResponse;
            }

            baseResponse.StatusCode = StatusCode.NoOk;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<State>()
            {
                Description = $"[Read] : {e.Message}"
            };
        }
    }

    public async Task<BaseResponse<bool>> Update(int id, State statenew)
    {
        var baseResponse = new BaseResponse<bool>();
        try
        {
            var states = await _stateRepository.ReadAll();
            var state = await _stateRepository.Read(id);
            if (state != null)
            {
                foreach (var item in states)
                {
                    if (item.Name == statenew.Name)
                    {
                        baseResponse.StatusCode = StatusCode.NoOk;
                        return baseResponse;
                    }
                }
                state.Name = statenew.Name;
                await _stateRepository.Update(state.Id, state);
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

    public async Task<BaseResponse<List<State>>> ReadAll()
    {
        var baseResponse = new BaseResponse<List<State>>();
        try
        {
            var states = await _stateRepository.ReadAll();
            if (states.Count > 0)
            {
                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = states;
                return baseResponse;
            }
            baseResponse.StatusCode = StatusCode.NoOk;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<List<State>>()
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
            var state = await _stateRepository.Read(id);
            if (state != null)
            {
                await _stateRepository.Delete(state);
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