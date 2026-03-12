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

        [HttpGet("Schema/ComponentManifest")]
        public ActionResult<ComponentManifest> ComponentManifestSchema()
        {
            return Ok(
                new ComponentManifest
                {
                    Id = "65f001122334455667788990",
                    ProjectId = "65f001122334455667788991",
                    DefaultVariantId = "65f001122334455667788992",
                    VariantIds = ["65f001122334455667788992", "65f001122334455667788993"],
                    BaseProps =
                    [
                        new ComponentManifestPropsStyleProperty
                        {
                            Value =
                            [
                                new ComponentManifestPropsStyle
                                {
                                    DesignTokenIds = ["65f0011223344556677889a1"],
                                    Key = "backgroundColor",
                                    Name = "Background Color",
                                    CssProperty = "background-color",
                                    ValueType = StyleValueType.String,
                                    Value = new ComponentManifestPropsStyleValue
                                    {
                                        DesignTokenId = "65f0011223344556677889a1",
                                        DesignTokenValueId = "65f0011223344556677889b1",
                                        Kind = ComponentManifestPropsStyleValueKind.DesignToken,
                                        ValueType = StyleValueType.String,
                                        StringValue = "#111111",
                                        NumberValue = null,
                                    },
                                    Unit = null,
                                },
                            ],
                        },
                        new ComponentManifestPropsBehaviorProperty
                        {
                            Value = [new ComponentManifestPropsBehavior { Key = "disabled" }],
                        },
                    ],
                    TagName = "Button",
                    Name = "Button",
                    CreatedTime = DateTime.UtcNow,
                    UpdatedTime = DateTime.UtcNow,
                    DeletedTime = null,
                }
            );
        }

        [HttpGet("Schema/ComponentManifestVariant")]
        public ActionResult<ComponentManifestVariant> ComponentManifestVariantSchema()
        {
            return Ok(
                new ComponentManifestVariant
                {
                    Id = "65f001122334455667788992",
                    ProjectId = "65f001122334455667788991",
                    ComponentManifestId = "65f001122334455667788990",
                    PropsOverride =
                    [
                        new ComponentManifestPropsBehaviorProperty
                        {
                            Value = [new ComponentManifestPropsBehavior { Key = "disabled" }],
                        },
                    ],
                    Key = "default",
                    Name = "Default",
                    Order = 0,
                    CreatedTime = DateTime.UtcNow,
                    UpdatedTime = DateTime.UtcNow,
                    DeletedTime = null,
                }
            );
        }

        [HttpGet("Schema/ComponentManifestProps")]
        public ActionResult<ComponentManifestProps> ComponentManifestPropsSchema()
        {
            return Ok(
                new ComponentManifestPropsStyleProperty
                {
                    Value =
                    [
                        new ComponentManifestPropsStyle
                        {
                            DesignTokenIds = ["65f0011223344556677889a1"],
                            Key = "backgroundColor",
                            Name = "Background Color",
                            CssProperty = "background-color",
                            ValueType = StyleValueType.String,
                            Value = new ComponentManifestPropsStyleValue
                            {
                                DesignTokenId = "65f0011223344556677889a1",
                                DesignTokenValueId = "65f0011223344556677889b1",
                                Kind = ComponentManifestPropsStyleValueKind.DesignToken,
                                ValueType = StyleValueType.String,
                                StringValue = "#111111",
                                NumberValue = null,
                            },
                            Unit = null,
                        },
                    ],
                }
            );
        }

        [HttpGet("Schema/ComponentManifestPropsStyle")]
        public ActionResult<ComponentManifestPropsStyle> ComponentManifestPropsStyleSchema()
        {
            return Ok(
                new ComponentManifestPropsStyle
                {
                    DesignTokenIds = ["65f0011223344556677889a1"],
                    Key = "backgroundColor",
                    Name = "Background Color",
                    CssProperty = "background-color",
                    ValueType = StyleValueType.String,
                    Value = new ComponentManifestPropsStyleValue
                    {
                        DesignTokenId = "65f0011223344556677889a1",
                        DesignTokenValueId = "65f0011223344556677889b1",
                        Kind = ComponentManifestPropsStyleValueKind.DesignToken,
                        ValueType = StyleValueType.String,
                        StringValue = "#111111",
                        NumberValue = null,
                    },
                    Unit = null,
                }
            );
        }

        [HttpGet("Schema/ComponentManifestPropsStyleValue")]
        public ActionResult<ComponentManifestPropsStyleValue> ComponentManifestPropsStyleValueSchema()
        {
            return Ok(
                new ComponentManifestPropsStyleValue
                {
                    DesignTokenId = "65f0011223344556677889a1",
                    DesignTokenValueId = "65f0011223344556677889b1",
                    Kind = ComponentManifestPropsStyleValueKind.DesignToken,
                    ValueType = StyleValueType.String,
                    StringValue = "#111111",
                    NumberValue = null,
                }
            );
        }

        [HttpGet("Schema/ComponentManifestPropsBehavior")]
        public ActionResult<ComponentManifestPropsBehavior> ComponentManifestPropsBehaviorSchema()
        {
            return Ok(new ComponentManifestPropsBehavior { Key = "disabled" });
        }
    }
}
