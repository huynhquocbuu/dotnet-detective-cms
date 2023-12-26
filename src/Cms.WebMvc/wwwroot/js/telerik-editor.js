function editorLoader(tagElement){
    //var baseServiceUrl = "https://demos.telerik.com/kendo-ui/service";
    //var baseServiceUrl = "http://localhost:5104/Api";
    var baseServiceUrl = "/Api";
    
    var imageBrowserRead = baseServiceUrl + "/ImageBrowser/Read";
    var imageBrowserDestroy = baseServiceUrl + "/ImageBrowser/Destroy";
    var imageBrowserCreate = baseServiceUrl + "/ImageBrowser/Create";
    var imageBrowserThumbnailUrl = baseServiceUrl + "/ImageBrowser/Thumbnail";
    var imageBrowserUploadUrl = baseServiceUrl + "/ImageBrowser/Upload";
    //var imageBrowserImageUrl = baseServiceUrl + "/ImageBrowser/Image?path={0}";
    //var imageBrowserImageUrl = "/contents/{0}";
    
    var fileBrowserRead = baseServiceUrl + "/FileBrowser/Read";
    var fileBrowserDestroy = baseServiceUrl + "/FileBrowser/Destroy";
    var fileBrowserCreate = baseServiceUrl + "/FileBrowser/Create";
    var fileBrowserUploadUrl = baseServiceUrl + "/FileBrowser/Upload";
    var fileBrowserFileUrl = baseServiceUrl + "/FileBrowser/File?fileName={0}";
    
    tagElement.kendoEditor({
        encoded: false,
        tools: [
            "bold",
            "italic",
            "underline",
            "undo",
            "redo",
            "strikethrough",
            "justifyLeft",
            "justifyCenter",
            "justifyRight",
            "justifyFull",
            "insertUnorderedList",
            "insertOrderedList",
            "insertUpperRomanList",
            "insertLowerRomanList",
            "indent",
            "outdent",
            "createLink",
            "unlink",
            "insertImage",
            "insertFile",
            "subscript",
            "superscript",
            "tableWizard",
            "createTable",
            "addRowAbove",
            "addRowBelow",
            "addColumnLeft",
            "addColumnRight",
            "deleteRow",
            "deleteColumn",
            "mergeCellsHorizontally",
            "mergeCellsVertically",
            "splitCellHorizontally",
            "splitCellVertically",
            "tableAlignLeft",
            "tableAlignCenter",
            "tableAlignRight",
            "viewHtml",
            "formatting",
            "cleanFormatting",
            "copyFormat",
            "applyFormat",
            "fontName",
            "fontSize",
            "foreColor",
            "backColor",
            "print"
        ],
        imageBrowser: {
            messages: {
                dropFilesHere: "Drop files here"
            },
            transport: {
                read: imageBrowserRead,
                destroy: {
                    url: imageBrowserDestroy,
                    type: "POST"
                },
                create: {
                    url: imageBrowserCreate,
                    type: "POST"
                },
                thumbnailUrl: imageBrowserThumbnailUrl,
                uploadUrl: imageBrowserUploadUrl,
                //imageUrl: imageBrowserImageUrl
                imageUrl: function(e){
                    //console.log(e);
                    var normalizedUrl = e.replace(/%2F/g, "/");
                    return "/contents/" + normalizedUrl;
                }
            }
        },
        fileBrowser: {
            messages: {
                dropFilesHere: "Drop files here"
            },
            transport: {
                read: fileBrowserRead,
                destroy: {
                    url: fileBrowserDestroy,
                    type: "POST"
                },
                create: {
                    url: fileBrowserCreate,
                    type: "POST"
                },
                uploadUrl: fileBrowserUploadUrl,
                fileUrl: fileBrowserFileUrl
            }
        },
        resizable: {
            content: true,
            toolbar: false
        }
    });
}