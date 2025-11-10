import csv
from pathlib import Path
import xml.etree.ElementTree as ET


ROOT = Path(__file__).resolve().parent
PACKAGES_CONFIG = ROOT / "WebGestor" / "packages.config"
VBPROJ = ROOT / "WebGestor" / "WebGestor.vbproj"
OUTPUT = ROOT / "deps_licenses_full.csv"


LICENSE_DATA = {
    ("Antlr", "3.5.0.2"): {
        "license": "BSD-3-Clause",
        "license_url": "https://www.antlr3.org/license.html",
        "redistributable": "yes",
        "notes": "",
    },
    ("AspNet.ScriptManager.bootstrap", "3.3.7"): {
        "license": "MIT",
        "license_url": "https://licenses.nuget.org/MIT",
        "redistributable": "yes",
        "notes": "",
    },
    ("AspNet.ScriptManager.jQuery", "3.3.1"): {
        "license": "MIT",
        "license_url": "https://licenses.nuget.org/MIT",
        "redistributable": "yes",
        "notes": "",
    },
    ("bootstrap", "3.3.7"): {
        "license": "MIT",
        "license_url": "https://github.com/twbs/bootstrap/blob/v3.3.7/LICENSE",
        "redistributable": "yes",
        "notes": "",
    },
    ("BouncyCastle", "1.8.9"): {
        "license": "MIT",
        "license_url": "https://www.bouncycastle.org/csharp/licence.html",
        "redistributable": "yes",
        "notes": "",
    },
    ("ClosedXML", "0.102.2"): {
        "license": "MIT",
        "license_url": "https://github.com/ClosedXML/ClosedXML/blob/0.102.2/LICENSE",
        "redistributable": "yes",
        "notes": "",
    },
    ("DocumentFormat.OpenXml", "2.16.0"): {
        "license": "MIT",
        "license_url": "https://licenses.nuget.org/MIT",
        "redistributable": "yes",
        "notes": "",
    },
    ("EPPlus", "6.2.6"): {
        "license": "Polyform-Noncommercial-1.0.0",
        "license_url": "https://licenses.nuget.org/Polyform-Noncommercial-1.0.0",
        "redistributable": "no",
        "notes": "Non-commercial license",
    },
    ("EPPlus.Interfaces", "6.1.1"): {
        "license": "Polyform-Noncommercial-1.0.0",
        "license_url": "https://licenses.nuget.org/Polyform-Noncommercial-1.0.0",
        "redistributable": "no",
        "notes": "Non-commercial license",
    },
    ("EPPlus.System.Drawing", "6.1.1"): {
        "license": "Polyform-Noncommercial-1.0.0",
        "license_url": "https://licenses.nuget.org/Polyform-Noncommercial-1.0.0",
        "redistributable": "no",
        "notes": "Non-commercial license",
    },
    ("ExcelNumberFormat", "1.1.0"): {
        "license": "MIT",
        "license_url": "https://licenses.nuget.org/MIT",
        "redistributable": "yes",
        "notes": "",
    },
    ("Irony.NetCore", "1.0.11"): {
        "license": "MIT",
        "license_url": "https://licenses.nuget.org/MIT",
        "redistributable": "yes",
        "notes": "",
    },
    ("iTextSharp", "5.5.13.3"): {
        "license": "AGPL-3.0-only",
        "license_url": "https://www.gnu.org/licenses/agpl-3.0.html",
        "redistributable": "unknown",
        "notes": "Strong copyleft license",
    },
    ("jQuery", "3.3.1"): {
        "license": "MIT",
        "license_url": "https://jquery.org/license/",
        "redistributable": "yes",
        "notes": "",
    },
    ("Microsoft.AspNet.FriendlyUrls.Core", "1.0.2"): {
        "license": "Apache-2.0",
        "license_url": "https://licenses.nuget.org/Apache-2.0",
        "redistributable": "yes",
        "notes": "",
    },
    ("Microsoft.AspNet.FriendlyUrls.Core.es", "1.0.2"): {
        "license": "Apache-2.0",
        "license_url": "https://licenses.nuget.org/Apache-2.0",
        "redistributable": "yes",
        "notes": "",
    },
    ("Microsoft.AspNet.ScriptManager.MSAjax", "5.0.0"): {
        "license": "Apache-2.0",
        "license_url": "https://licenses.nuget.org/Apache-2.0",
        "redistributable": "yes",
        "notes": "",
    },
    ("Microsoft.AspNet.ScriptManager.WebForms", "5.0.0"): {
        "license": "Apache-2.0",
        "license_url": "https://licenses.nuget.org/Apache-2.0",
        "redistributable": "yes",
        "notes": "",
    },
    ("Microsoft.AspNet.Web.Optimization", "1.1.3"): {
        "license": "Apache-2.0",
        "license_url": "https://licenses.nuget.org/Apache-2.0",
        "redistributable": "yes",
        "notes": "",
    },
    ("Microsoft.AspNet.Web.Optimization.es", "1.1.3"): {
        "license": "Apache-2.0",
        "license_url": "https://licenses.nuget.org/Apache-2.0",
        "redistributable": "yes",
        "notes": "",
    },
    ("Microsoft.AspNet.Web.Optimization.WebForms", "1.1.3"): {
        "license": "Apache-2.0",
        "license_url": "https://licenses.nuget.org/Apache-2.0",
        "redistributable": "yes",
        "notes": "",
    },
    ("Microsoft.CodeDom.Providers.DotNetCompilerPlatform", "2.0.0"): {
        "license": "Apache-2.0",
        "license_url": "https://licenses.nuget.org/Apache-2.0",
        "redistributable": "yes",
        "notes": "",
    },
    ("Microsoft.Web.Infrastructure", "1.0.0.0"): {
        "license": "Apache-2.0",
        "license_url": "https://licenses.nuget.org/Apache-2.0",
        "redistributable": "yes",
        "notes": "",
    },
    ("Modernizr", "2.8.3"): {
        "license": "MIT",
        "license_url": "https://github.com/Modernizr/Modernizr/blob/v2.8.3/LICENSE",
        "redistributable": "yes",
        "notes": "",
    },
    ("Newtonsoft.Json", "11.0.1"): {
        "license": "MIT",
        "license_url": "https://licenses.nuget.org/MIT",
        "redistributable": "yes",
        "notes": "",
    },
    ("QRCoder", "1.4.3"): {
        "license": "MIT",
        "license_url": "https://licenses.nuget.org/MIT",
        "redistributable": "yes",
        "notes": "",
    },
    ("SixLabors.Fonts", "1.0.0"): {
        "license": "Apache-2.0",
        "license_url": "https://licenses.nuget.org/Apache-2.0",
        "redistributable": "yes",
        "notes": "",
    },
    ("System.Buffers", "4.5.1"): {
        "license": "MIT",
        "license_url": "https://licenses.nuget.org/MIT",
        "redistributable": "yes",
        "notes": "",
    },
    ("System.IO.Packaging", "6.0.0"): {
        "license": "MIT",
        "license_url": "https://licenses.nuget.org/MIT",
        "redistributable": "yes",
        "notes": "",
    },
    ("System.Memory", "4.5.4"): {
        "license": "MIT",
        "license_url": "https://licenses.nuget.org/MIT",
        "redistributable": "yes",
        "notes": "",
    },
    ("System.Net.Http", "4.3.4"): {
        "license": "MIT",
        "license_url": "https://licenses.nuget.org/MIT",
        "redistributable": "yes",
        "notes": "",
    },
    ("System.Numerics.Vectors", "4.5.0"): {
        "license": "MIT",
        "license_url": "https://licenses.nuget.org/MIT",
        "redistributable": "yes",
        "notes": "",
    },
    ("System.Runtime.CompilerServices.Unsafe", "4.7.0"): {
        "license": "MIT",
        "license_url": "https://licenses.nuget.org/MIT",
        "redistributable": "yes",
        "notes": "",
    },
    ("System.Security.Cryptography.Algorithms", "4.3.0"): {
        "license": "MIT",
        "license_url": "https://licenses.nuget.org/MIT",
        "redistributable": "yes",
        "notes": "",
    },
    ("System.Security.Cryptography.Encoding", "4.3.0"): {
        "license": "MIT",
        "license_url": "https://licenses.nuget.org/MIT",
        "redistributable": "yes",
        "notes": "",
    },
    ("System.Security.Cryptography.Primitives", "4.3.0"): {
        "license": "MIT",
        "license_url": "https://licenses.nuget.org/MIT",
        "redistributable": "yes",
        "notes": "",
    },
    ("System.Security.Cryptography.X509Certificates", "4.3.0"): {
        "license": "MIT",
        "license_url": "https://licenses.nuget.org/MIT",
        "redistributable": "yes",
        "notes": "",
    },
    ("System.Text.RegularExpressions", "4.3.1"): {
        "license": "MIT",
        "license_url": "https://licenses.nuget.org/MIT",
        "redistributable": "yes",
        "notes": "",
    },
    ("WebGrease", "1.6.0"): {
        "license": "Apache-2.0",
        "license_url": "https://licenses.nuget.org/Apache-2.0",
        "redistributable": "yes",
        "notes": "",
    },
    ("XLParser", "1.5.2"): {
        "license": "MIT",
        "license_url": "https://licenses.nuget.org/MIT",
        "redistributable": "yes",
        "notes": "",
    },
    ("AjaxControlToolkit", "1.0.10618.0"): {
        "license": "MS-PL",
        "license_url": "https://licenses.nuget.org/MS-PL",
        "redistributable": "yes",
        "notes": "",
    },
    ("tessnet2_32", "2.0.4.0"): {
        "license": "LGPL-2.1-or-later",
        "license_url": "https://www.gnu.org/licenses/old-licenses/lgpl-2.1.html",
        "redistributable": "unknown",
        "notes": "Wrapped Tesseract OCR library",
    },
    ("Interop.Excel", "unknown"): {
        "license": "unknown",
        "license_url": "",
        "redistributable": "unknown",
        "notes": "Microsoft Office interop assembly",
    },
    ("Interop.TAPI3Lib", "unknown"): {
        "license": "unknown",
        "license_url": "",
        "redistributable": "unknown",
        "notes": "COM interop assembly",
    },
    ("Interop.TERMMGRLib", "unknown"): {
        "license": "unknown",
        "license_url": "",
        "redistributable": "unknown",
        "notes": "COM interop assembly",
    },
}


def ensure_entry(package, version):
    key = (package, version)
    info = LICENSE_DATA.setdefault(
        key,
        {
            "license": "unknown",
            "license_url": "",
            "redistributable": "unknown",
            "notes": "",
        },
    )
    info.setdefault("detection_sources", set())
    info.setdefault("usage_paths", set())
    return key, info


def attach_metadata():
    for key, info in LICENSE_DATA.items():
        info.setdefault("detection_sources", set())
        info.setdefault("usage_paths", set())


def parse_packages_config():
    if not PACKAGES_CONFIG.exists():
        return
    tree = ET.parse(PACKAGES_CONFIG)
    root = tree.getroot()
    for pkg in root.findall("package"):
        package_id = pkg.attrib.get("id")
        version = pkg.attrib.get("version", "unknown")
        key, info = ensure_entry(package_id, version)
        info["detection_sources"].add("packages.config")
        info["usage_paths"].add(str(PACKAGES_CONFIG.relative_to(ROOT)))


def parse_vbproj():
    if not VBPROJ.exists():
        return
    tree = ET.parse(VBPROJ)
    ns = {"msb": "http://schemas.microsoft.com/developer/msbuild/2003"}
    mapping = {
        "AjaxControlToolkit": ("AjaxControlToolkit", "1.0.10618.0"),
        "Antlr3.Runtime": ("Antlr", "3.5.0.2"),
        "AspNet.ScriptManager.bootstrap": ("AspNet.ScriptManager.bootstrap", "3.3.7"),
        "AspNet.ScriptManager.jQuery": ("AspNet.ScriptManager.jQuery", "3.3.1"),
        "Microsoft.ScriptManager.MSAjax": ("Microsoft.AspNet.ScriptManager.MSAjax", "5.0.0"),
        "Microsoft.ScriptManager.WebForms": ("Microsoft.AspNet.ScriptManager.WebForms", "5.0.0"),
        "Microsoft.Web.Infrastructure": ("Microsoft.Web.Infrastructure", "1.0.0.0"),
        "Microsoft.AspNet.Web.Optimization.WebForms": ("Microsoft.AspNet.Web.Optimization.WebForms", "1.1.3"),
        "Microsoft.AspNet.FriendlyUrls": ("Microsoft.AspNet.FriendlyUrls.Core", "1.0.2"),
        "Microsoft.CodeDom.Providers.DotNetCompilerPlatform": (
            "Microsoft.CodeDom.Providers.DotNetCompilerPlatform",
            "2.0.0",
        ),
        "Newtonsoft.Json": ("Newtonsoft.Json", "11.0.1"),
        "System.Web.Optimization": ("Microsoft.AspNet.Web.Optimization", "1.1.3"),
        "tessnet2_32": ("tessnet2_32", "2.0.4.0"),
        "WebGrease": ("WebGrease", "1.6.0"),
        "Interop.Excel": ("Interop.Excel", "unknown"),
    }
    # Interop assemblies referenced as Content
    interop_content = {
        "bin\\Interop.TAPI3Lib.dll": ("Interop.TAPI3Lib", "unknown"),
        "bin\\Interop.TERMMGRLib.dll": ("Interop.TERMMGRLib", "unknown"),
    }

    for reference in tree.findall(".//msb:Reference", ns):
        include = reference.attrib.get("Include", "")
        name = include.split(",", 1)[0]
        if name in mapping:
            package, version = mapping[name]
            key, info = ensure_entry(package, version)
            info["detection_sources"].add("vbproj")
            info["usage_paths"].add(str(VBPROJ.relative_to(ROOT)))

    for content in tree.findall(".//msb:Content", ns):
        include = content.attrib.get("Include", "")
        if include in interop_content:
            package, version = interop_content[include]
            key, info = ensure_entry(package, version)
            info["detection_sources"].add("vbproj")
            info["usage_paths"].add(str(VBPROJ.relative_to(ROOT)))


def write_csv():
    fieldnames = [
        "package",
        "version",
        "license",
        "license_url",
        "detection_source",
        "usage_paths",
        "redistributable",
        "notes",
    ]

    rows = []
    for (package, version), info in LICENSE_DATA.items():
        detection_sources = info.get("detection_sources", set())
        usage_paths = info.get("usage_paths", set())
        rows.append(
            {
                "package": package,
                "version": version,
                "license": info.get("license", "unknown"),
                "license_url": info.get("license_url", ""),
                "detection_source": "|".join(sorted(detection_sources)) if detection_sources else "",
                "usage_paths": "|".join(sorted(usage_paths)) if usage_paths else "",
                "redistributable": info.get("redistributable", "unknown"),
                "notes": info.get("notes", ""),
            }
        )

    rows.sort(key=lambda r: (r["package"].lower(), r["version"]))

    with OUTPUT.open("w", newline="", encoding="utf-8") as fh:
        writer = csv.DictWriter(fh, fieldnames=fieldnames)
        writer.writeheader()
        writer.writerows(rows)


def main():
    attach_metadata()
    parse_packages_config()
    parse_vbproj()
    write_csv()


if __name__ == "__main__":
    main()
