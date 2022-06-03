using Catridges.DAL.Interfaces;
using Catridges.Domain.Entity;
using Catridges.Domain.Enum;
using Catridges.Domain.Response;
using Catridges.Service.Interfases;

namespace Catridges.Service.Implementations;

public class PrinterService : IPrinterService
{
    private readonly IPrinterRepository _printerRepository;

    public PrinterService(IPrinterRepository printerRepository)
    {
        _printerRepository = printerRepository;
    }

    public async Task<BaseResponse<bool>> Create(Printer? printer)
    {
        var baseResponse = new BaseResponse<bool>();
        try
        {
            if (printer != null)
            {
                var printers = await _printerRepository.ReadAll();
                foreach (var item in printers)
                {
                    if (printer.Model == item.Model)
                    {
                        baseResponse.StatusCode = StatusCode.NoOk;
                        return baseResponse;
                    }
                }
                await _printerRepository.Create(printer);
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

    public async Task<BaseResponse<Printer>> Read(int id)
    {
        var baseResponse = new BaseResponse<Printer>();
        try
        {
            var printer = await _printerRepository.Read(id);
            if (printer != null)
            {
                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = printer;
                return baseResponse;
            }

            baseResponse.StatusCode = StatusCode.NoOk;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<Printer>()
            {
                Description = $"[Read] : {e.Message}"
            };
        }
    }

    public async Task<BaseResponse<bool>> Update(int id, Printer printernew)
    {
        var baseResponse = new BaseResponse<bool>();
        try
        {
            var printers = await _printerRepository.ReadAll();
            var printer = await _printerRepository.Read(id);
            if (printer != null)
            {
                foreach (var item in printers)
                {
                    if (item.Model == printernew.Model)
                    {
                        baseResponse.StatusCode = StatusCode.NoOk;
                        return baseResponse;
                    }
                }
                printer.Model = printernew.Model;
                await _printerRepository.Update(printer.Id,printer);
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

    public async Task<BaseResponse<List<Printer>>> ReadAll()
    {
        var baseResponse = new BaseResponse<List<Printer>>();
        try
        {
            var printers = await _printerRepository.ReadAll();
            if (printers.Count > 0)
            {
                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = printers;
                return baseResponse;
            }
            baseResponse.StatusCode = StatusCode.NoOk;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<List<Printer>>()
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
            var printer = await _printerRepository.Read(id);
            if (printer != null)
            {
                await _printerRepository.Delete(printer);
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