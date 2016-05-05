using UnityEngine;
using System.Collections;

public class VimeoWrapper : MonoBehaviour
{

    public string vimeoID = "161794986";
    public float widthFraction = 1;
    public float heightFraction = 1;

    private GameObject vimeoWrapper;
    private UniWebView uniWebView;
    
    public void Start()
    {
        vimeoWrapper = new GameObject("Video display");
        uniWebView = vimeoWrapper.AddComponent<UniWebView>();
        uniWebView.autoShowWhenLoadComplete = true;
        OnClickListener();
    }
    public void OnClickListener()
    {
        float width = UniWebViewHelper.screenWidth;
        float height = UniWebViewHelper.screenHeight;
        string test = createVimeoURL(vimeoID, (int) (width * widthFraction), (int)(height * heightFraction));
        uniWebView.insets = new UniWebViewEdgeInsets((int) (width * (1 - widthFraction) / 2),(int)(height * (1 - heightFraction) / 2),(int)( width * (1 - widthFraction) / 2),(int) (height * (1 - heightFraction) / 2));
        uniWebView.SetUseWideViewPort(false);
        uniWebView.LoadHTMLString(test, null);

    }

    private string createVimeoURL(string ID, int width, int height)
    {
        //< iframe src = "https://player.vimeo.com/video/161794986?title=0&byline=0&portrait=0" 
        //width = "1024" height = "640" 
        //frameborder = "0" 
        //webkitallowfullscreen 
        //mozallowfullscreen 
        //allowfullscreen ></ iframe >
        string autoplay_script = "<script src="+@"https://f.vimeocdn.com/js/froogaloop2.min.js"+"></script>";
        string jquery = "<script src="+@"https://ajax.googleapis.com/ajax/libs/jquery/2.2.2/jquery.min.js"+"></script>";
        string iframe_start = "<iframe id=\'player1\' src=";
        string VimeoPrefix = "\"https://player.vimeo.com/video/";
        string VimeoSuffix = "?api=1&player_id=player1&loop=1&title=0&byline=0&portrait=0\"";
        string widthString = " width=\""+ width +"\"";
        string heightString = " height=\""+ height +"\"";
        string frameBorder = " frameborder=\"0\"";
        string fullScreenSettings = " webkitallowfullscreen mozallowfullscreen allowfullscreen";
        string iframe_end = "></iframe>";
        string final = autoplay_script + jquery + iframe_start + VimeoPrefix + ID + VimeoSuffix + widthString + heightString + frameBorder + fullScreenSettings + iframe_end + AutoplayJS();
        
        return final; 
    }

    private string AutoplayJS()
    {
        string code = "<script>$(document).ready(function() { var iframe = $(\'#player1\')[0]; var player = $f(iframe); player.addEvent(\'ready\', function() { player.api(\'play\'); }); });</script>";
        return code;
    }

    private string AutoPlayHTML5()
    {
        string jquery = "<script src=" + @"https://ajax.googleapis.com/ajax/libs/jquery/2.2.2/jquery.min.js" + "></script>";
        string code = "<script>$(document).ready(function(){ var player = document.getElementById(\'player1\'); player.play(); });</script>";
        return jquery + code;
    }
}
