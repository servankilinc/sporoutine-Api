using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.GlobalExceptionHandler.PDetails;

public class BusinessProblemDetails : ProblemDetails
{
    public override string ToString() => JsonConvert.SerializeObject(this); // serialize this class by all props

}
