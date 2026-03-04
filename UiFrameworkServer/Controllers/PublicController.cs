using Microsoft.AspNetCore.Mvc;
using UiFrameworkServer.Contract.Enums.ComponentManifests;
using UiFrameworkServer.Contract.Enums.Designs;
using UiFrameworkServer.Contract.Models.ComponentManifests;

namespace UiFrameworkServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublicController : BaseController
    {
        public PublicController(IServiceProvider serviceProvider)
            : base(serviceProvider) { }

        [HttpGet("Schema/ComponentManifestDefinition")]
        public ActionResult<ComponentManifestDefinition> ComponentManifestDefinitionSchema()
        {
            return Ok(
                new ComponentManifestDefinition
                {
                    Name = "Button",
                    TagName = "Button",
                    BaseProps =
                    [
                        new ComponentManifestPropsStylePropertyDefinition
                        {
                            Value =
                            [
                                new ComponentManifestPropsStyleDefinition
                                {
                                    Key = "backgroundColor",
                                    Name = "Background Color",
                                    CssProperty = "background-color",
                                    ValueType = StyleValueType.String,
                                    Unit = null,
                                },
                            ],
                        },
                        new ComponentManifestPropsBehaviorPropertyDefinition
                        {
                            Value =
                            [
                                new ComponentManifestPropsBehaviorDefinition { Key = "disabled" },
                            ],
                        },
                    ],
                    Variants = ["default", "ghost"],
                    VariantOverrides = new(),
                }
            );
        }

        [HttpGet("Schema/ComponentManifestPropsKind")]
        public ActionResult<ComponentManifestPropsKind> PropsKind()
        {
            return Ok(ComponentManifestPropsKind.Style);
        }

        [HttpGet("Schema/ComponentManifestPropsStyleDefinition")]
        public ActionResult<ComponentManifestPropsStyleDefinition> StyleDefinition()
        {
            return Ok(
                new ComponentManifestPropsStyleDefinition
                {
                    Key = "backgroundColor",
                    Name = "Background Color",
                    CssProperty = "background-color",
                    ValueType = StyleValueType.String,
                    Unit = null,
                }
            );
        }

        [HttpGet("Schema/ComponentManifestPropsBehaviorDefinition")]
        public ActionResult<ComponentManifestPropsBehaviorDefinition> BehaviorDefinition()
        {
            return Ok(new ComponentManifestPropsBehaviorDefinition { Key = "disabled" });
        }

        [HttpGet("Schema/ComponentManifestPropsStyleValueKind")]
        public ActionResult<ComponentManifestPropsStyleValueKind> StyleValueKind()
        {
            return Ok(ComponentManifestPropsStyleValueKind.Unset);
        }

        [HttpGet("Schema/StyleValueType")]
        public ActionResult<StyleValueType> StyleValueTypeSchema()
        {
            return Ok(StyleValueType.String);
        }

        [HttpGet("Schema/StyleValueUnit")]
        public ActionResult<StyleValueUnit> StyleValueUnitSchema()
        {
            return Ok(StyleValueUnit.Px);
        }
    }
}
