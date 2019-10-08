// Copyright (c) 2011 Bob Berkebile (pixelplacment)
// Please direct any bugs/comments/suggestions to http://pixelplacement.com

using UnityEngine;

public enum EaseType{
	easeInQuad,
	easeOutQuad,
	easeInOutQuad,
	easeInCubic,
	easeOutCubic,
	easeInOutCubic,
	easeInQuart,
	easeOutQuart,
	easeInOutQuart,
	easeInQuint,
	easeOutQuint,
	easeInOutQuint,
	easeInSine,
	easeOutSine,
	easeInOutSine,
	easeInExpo,
	easeOutExpo,
	easeInOutExpo,
	easeInCirc,
	easeOutCirc,
	easeInOutCirc,
	linear,
	spring,
	easeInBounce,
	easeOutBounce,
	easeInOutBounce,
	easeInBack,
	easeOutBack,
	easeInOutBack,
	easeInElastic,
	easeOutElastic,
	easeInOutElastic,
	punch
}

public enum LoopType{
	/// <summary>
	/// Rewind and replay.
	/// </summary>
	loop,
	/// <summary>
	/// Ping pong the animation back and forth.
	/// </summary>
	pingPong
}

public static class iTweenExtensions
{	
	/// <summary>
	/// Instantly changes an AudioSource's volume and pitch then returns it to it's starting volume and pitch over time. Default AudioSource attached to GameObject will be used.
	/// </summary>
	/// <param name="volume"> 
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="pitch"> 
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="time"> 
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay"> 
	/// A <see cref="System.Single"/>
	/// </param>
	public static void AudioFrom(this GameObject go,float volume,float pitch,float time,float delay){
		iTween.AudioFrom(go,iTween.Hash("volume",volume,"pitch",pitch,"time",time,"delay",delay));
	}
	
	/// <summary>
	/// Instantly changes an AudioSource's volume and pitch then returns it to it's starting volume and pitch over time. Default AudioSource attached to GameObject will be used.
	/// </summary>
	/// <param name="volume"> 
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="pitch"> 
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="time"> 
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay"> 
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="looptype">
	/// A <see cref="LoopType"/>
	/// </param>
	public static void AudioFrom(this GameObject go,float volume,float pitch,float time,float delay,LoopType loopType){
		iTween.AudioFrom(go,iTween.Hash("volume",volume,"pitch",pitch,"time",time,"delay",delay,"looptype",loopType.ToString()));
	}
	
	/// <summary>
	/// Fades volume and pitch of an AudioSource.  Default AudioSource attached to GameObject will be used. 
	/// </summary>
	/// <param name="volume">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="pitch">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void AudioTo(this GameObject go,float volume,float pitch,float time,float delay){
		iTween.AudioTo(go,iTween.Hash("volume",volume,"pitch",pitch,"time",time,"delay",delay));
	}
	
	/// <summary>
	/// Fades volume and pitch of an AudioSource.  Default AudioSource attached to GameObject will be used. 
	/// </summary>
	/// <param name="volume">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="pitch">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="loopType">
	/// A <see cref="LoopType"/>
	/// </param>
	public static void AudioTo(this GameObject go,float volume,float pitch,float time,float delay,LoopType loopType){
		iTween.AudioTo(go,iTween.Hash("volume",volume,"pitch",pitch,"time",time,"delay",delay,"looptype",loopType.ToString()));
	}
	
	/// <summary>
	/// Similar to AudioTo but incredibly less expensive for usage inside the Update function or similar looping situations involving a "live" set of changing values. Does not utilize an EaseType. 
	/// </summary>
	/// <param name="volume">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="pitch">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void AudioUpdate(this GameObject go,float volume,float pitch,float time){
		iTween.AudioUpdate(go,volume,pitch,time);
	}
	
	/// <summary>
	/// Changes a GameObject's color values instantly then returns them to the provided properties over time.  If a GUIText or GUITexture component is attached, it will become the target of the animation.
	/// </summary>
	/// <param name="color">
	/// A <see cref="Color"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void ColorFrom(this GameObject go,Color color,float time,float delay){
		iTween.ColorFrom(go,iTween.Hash("color",color,"time",time,"delay",delay));	
	}
	
	/// <summary>
	/// Changes a GameObject's color values instantly then returns them to the provided properties over time.  If a GUIText or GUITexture component is attached, it will become the target of the animation.
	/// </summary>
	/// <param name="color">
	/// A <see cref="Color"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="loopType">
	/// A <see cref="LoopType"/>
	/// </param>
	public static void ColorFrom(this GameObject go,Color color,float time,float delay,LoopType loopType){
		iTween.ColorFrom(go,iTween.Hash("color",color,"time",time,"delay",delay,"looptype",loopType.ToString()));	
	}
	
	/// <summary>
	/// Changes a GameObject's color values over time.  If a GUIText or GUITexture component is attached, they will become the target of the animation.
	/// </summary>
	/// <param name="color">
	/// A <see cref="Color"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void ColorTo(this GameObject go,Color color,float time,float delay){
		iTween.ColorTo(go,iTween.Hash("color",color,"time",time,"delay",delay));	
	}
	
	/// <summary>
	/// Changes a GameObject's color values over time.  If a GUIText or GUITexture component is attached, they will become the target of the animation.
	/// </summary>
	/// <param name="color">
	/// A <see cref="Color"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="loopType">
	/// A <see cref="LoopType"/>
	/// </param>
	public static void ColorTo(this GameObject go,Color color,float time,float delay,LoopType loopType){
		iTween.ColorTo(go,iTween.Hash("color",color,"time",time,"delay",delay,"looptype",loopType.ToString()));	
	}
	
	/// <summary>
	/// Similar to ColorTo but incredibly less expensive for usage inside the Update function or similar looping situations involving a "live" set of changing values. Does not utilize an EaseType.
	/// </summary>
	/// <param name="color">
	/// A <see cref="Color"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void ColorUpdate(this GameObject go,Color color,float time){
		iTween.ColorUpdate(go,color,time);
	}
	
	/// <summary>
	/// Changes a GameObject's alpha value instantly then returns it to the provided alpha over time.  If a GUIText or GUITexture component is attached, it will become the target of the animation. Identical to using ColorFrom and using the "a" parameter. 
	/// </summary>
	/// <param name="alpha">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void FadeFrom(this GameObject go,float alpha,float time,float delay){
		iTween.FadeFrom(go,iTween.Hash("alpha",alpha,"time",time,"delay",delay));	
	}
	
	/// <summary>
	/// Changes a GameObject's alpha value instantly then returns it to the provided alpha over time.  If a GUIText or GUITexture component is attached, it will become the target of the animation. Identical to using ColorFrom and using the "a" parameter. 
	/// </summary>
	/// <param name="alpha">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="loopType">
	/// A <see cref="LoopType"/>
	/// </param>
	public static void FadeFrom(this GameObject go,float alpha,float time,float delay,LoopType loopType){
		iTween.FadeFrom(go,iTween.Hash("alpha",alpha,"time",time,"delay",delay,"looptype",loopType.ToString()));	
	}
	
	/// <summary>
	/// Changes a GameObject's alpha value over time.  If a GUIText or GUITexture component is attached, it will become the target of the animation. Identical to using ColorTo and using the "a" parameter.
	/// </summary>
	/// <param name="alpha">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void FadeTo(this GameObject go,float alpha,float time,float delay){
		iTween.FadeTo(go,iTween.Hash("alpha",alpha,"time",time,"delay",delay));	
	}
	
	/// <summary>
	/// Changes a GameObject's alpha value over time.  If a GUIText or GUITexture component is attached, it will become the target of the animation. Identical to using ColorTo and using the "a" parameter.
	/// </summary>
	/// <param name="alpha">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="loopType">
	/// A <see cref="LoopType"/>
	/// </param>
	public static void FadeTo(this GameObject go,float alpha,float time,float delay,LoopType loopType){
		iTween.FadeTo(go,iTween.Hash("alpha",alpha,"time",time,"delay",delay,"looptype",loopType.ToString()));	
	}
	
	/// <summary>
	/// Similar to FadeTo but incredibly less expensive for usage inside the Update function or similar looping situations involving a "live" set of changing values. Does not utilize an EaseType. 
	/// </summary>
	/// <param name="alpha">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void FadeUpdate(this GameObject go,float alpha,float time){
		iTween.FadeUpdate(go,alpha,time);	
	}
	
	/// <summary>
	/// Sets up a GameObject to avoid hiccups when an initial iTween is added. It's advisable to run this on every object you intend to run iTween on in its Start or Awake.
	/// </summary>
	public static void Init(this GameObject go){
		iTween.Init(go);
	}
	
	/// <summary>
	/// Instantly rotates a GameObject to look at the supplied Vector3 then returns it to it's starting rotation over time. 
	/// </summary>
	/// <param name="lookTarget">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void LookFrom(this GameObject go,Vector3 lookTarget,float time,float delay){
		iTween.LookFrom(go,iTween.Hash("lookTarget",lookTarget,"time",time,"delay",delay));
	}
	
	/// <summary>
	/// Instantly rotates a GameObject to look at the supplied Vector3 then returns it to it's starting rotation over time. 
	/// </summary>
	/// <param name="lookTarget">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	public static void LookFrom(this GameObject go,Vector3 lookTarget,float time,float delay,EaseType easeType){
		iTween.LookFrom(go,iTween.Hash("lookTarget",lookTarget,"time",time,"delay",delay,"easeType",easeType.ToString()));
	}
	
	/// <summary>
	/// Instantly rotates a GameObject to look at the supplied Vector3 then returns it to it's starting rotation over time. 
	/// </summary>
	/// <param name="lookTarget">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	/// <param name="looptype">
	/// A <see cref="LoopType"/>
	/// </param>
	public static void LookFrom(this GameObject go,Vector3 lookTarget,float time,float delay,EaseType easeType,LoopType looptype){
		iTween.LookFrom(go,iTween.Hash("lookTarget",lookTarget,"time",time,"delay",delay,"easeType",easeType.ToString(),"looptype",looptype.ToString()));
	}
	
	/// <summary>
	/// Rotates a GameObject to look at the supplied Vector3 over time.
	/// </summary>
	/// <param name="lookTarget">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void LookTo(this GameObject go,Vector3 lookTarget,float time,float delay){
		iTween.LookTo(go,iTween.Hash("lookTarget",lookTarget,"time",time,"delay",delay));
	}
	
	/// <summary>
	/// Rotates a GameObject to look at the supplied Vector3 over time.
	/// </summary>
	/// <param name="lookTarget">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	public static void LookTo(this GameObject go,Vector3 lookTarget,float time,float delay,EaseType easeType){
		iTween.LookTo(go,iTween.Hash("lookTarget",lookTarget,"time",time,"delay",delay,"easeType",easeType.ToString()));
	}
	
	/// <summary>
	/// Rotates a GameObject to look at the supplied Vector3 over time.
	/// </summary>
	/// <param name="lookTarget">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	/// <param name="loopType">
	/// A <see cref="LoopType"/>
	/// </param>
	public static void LookTo(this GameObject go,Vector3 lookTarget,float time,float delay,EaseType easeType,LoopType loopType){
		iTween.LookTo(go,iTween.Hash("lookTarget",lookTarget,"time",time,"delay",delay,"easeType",easeType.ToString(),"looptype",loopType.ToString()));
	}
	
	/// <summary>
	/// Similar to LookTo but incredibly less expensive for usage inside the Update function or similar looping situations involving a "live" set of changing values. Does not utilize an EaseType. 
	/// </summary>
	/// <param name="lookTarget">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void LookUpdate(this GameObject go,Vector3 lookTarget,float time){
		iTween.LookUpdate(go,lookTarget,time);
	}
	
	/// <summary>
	/// Translates a GameObject's position over time.
	/// </summary>
	/// <param name="amount">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void MoveAdd(this GameObject go,Vector3 amount,float time,float delay){
		iTween.MoveAdd(go,iTween.Hash("amount",amount,"time",time,"delay",delay));
	}
	
	/// <summary>
	/// Translates a GameObject's position over time.
	/// </summary>
	/// <param name="amount">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	public static void MoveAdd(this GameObject go,Vector3 amount,float time,float delay,EaseType easeType){
		iTween.MoveAdd(go,iTween.Hash("amount",amount,"time",time,"delay",delay,"easeType",easeType.ToString()));
	}
	
	/// <summary>
	/// Translates a GameObject's position over time.
	/// </summary>
	/// <param name="amount">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	/// <param name="loopType">
	/// A <see cref="LoopType"/>
	/// </param>
	public static void MoveAdd(this GameObject go,Vector3 amount,float time,float delay,EaseType easeType,LoopType loopType){
		iTween.MoveAdd(go,iTween.Hash("amount",amount,"time",time,"delay",delay,"easeType",easeType.ToString(),"looptype",loopType.ToString()));
	}
	
	/// <summary>
	/// Adds the supplied coordinates to a GameObject's position.
	/// </summary>
	/// <param name="amount">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void MoveBy(this GameObject go,Vector3 amount,float time,float delay){
		iTween.MoveBy(go,iTween.Hash("amount",amount,"time",time,"delay",delay));
	}
	
	/// <summary>
	/// Adds the supplied coordinates to a GameObject's position.
	/// </summary>
	/// <param name="amount">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	public static void MoveBy(this GameObject go,Vector3 amount,float time,float delay,EaseType easeType){
		iTween.MoveBy(go,iTween.Hash("amount",amount,"time",time,"delay",delay,"easeType",easeType.ToString()));
	}
	
	/// <summary>
	/// Adds the supplied coordinates to a GameObject's position.
	/// </summary>
	/// <param name="amount">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	/// <param name="loopType">
	/// A <see cref="LoopType"/>
	/// </param>
	public static void MoveBy(this GameObject go,Vector3 amount,float time,float delay,EaseType easeType,LoopType loopType){
		iTween.MoveBy(go,iTween.Hash("amount",amount,"time",time,"delay",delay,"easeType",easeType.ToString(),"looptype",loopType.ToString()));
	}
	
	/// <summary>
	/// Instantly changes a GameObject's position to a supplied destination then returns it to it's starting position over time.
	/// </summary>
	/// <param name="position">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void MoveFrom(this GameObject go,Vector3 position,float time,float delay){
		iTween.MoveFrom(go,iTween.Hash("position",position,"time",time,"delay",delay));
	}
	
	/// <summary>
	/// Instantly changes a GameObject's position to a supplied destination then returns it to it's starting position over time.
	/// </summary>
	/// <param name="position">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	public static void MoveFrom(this GameObject go,Vector3 position,float time,float delay,EaseType easeType){
		iTween.MoveFrom(go,iTween.Hash("position",position,"time",time,"delay",delay,"easeType",easeType.ToString()));
	}
	
	/// <summary>
	/// Instantly changes a GameObject's position to a supplied destination then returns it to it's starting position over time.
	/// </summary>
	/// <param name="position">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	/// <param name="loopType">
	/// A <see cref="LoopType"/>
	/// </param>
	public static void MoveFrom(this GameObject go,Vector3 position,float time,float delay,EaseType easeType,LoopType loopType){
		iTween.MoveFrom(go,iTween.Hash("position",position,"time",time,"delay",delay,"easeType",easeType.ToString(),"looptype",loopType.ToString()));
	}
	
	/// <summary>
	/// Changes a GameObject's position over time to a supplied destination.
	/// </summary>
	/// <param name="position">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void MoveTo(this GameObject go,Vector3 position,float time,float delay){
		iTween.MoveTo(go,iTween.Hash("position",position,"time",time,"delay",delay));
	}
	
	/// <summary>
	/// Changes a GameObject's position over time along a supplied path.
	/// </summary>
	/// <param name="path">
	/// A <see cref="Vector3[]"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void MoveTo(this GameObject go,Vector3[] path,float time,float delay){
		iTween.MoveTo(go,iTween.Hash("path",path,"time",time,"delay",delay));
	}
	
	/// <summary>
	/// Changes a GameObject's position over time along a supplied path.
	/// </summary>
	/// <param name="path">
	/// A <see cref="Transform[]"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void MoveTo(this GameObject go,Transform[] path,float time,float delay){
		iTween.MoveTo(go,iTween.Hash("path",path,"time",time,"delay",delay));
	}
	
	/// <summary>
	/// Changes a GameObject's position over time to a supplied destination.
	/// </summary>
	/// <param name="position">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	public static void MoveTo(this GameObject go,Vector3 position,float time,float delay,EaseType easeType){
		iTween.MoveTo(go,iTween.Hash("position",position,"time",time,"delay",delay,"easeType",easeType.ToString()));
	}
	
	/// <summary>
	/// Changes a GameObject's position over time along a supplied path.
	/// </summary>
	/// <param name="path">
	/// A <see cref="Vector3[]"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	public static void MoveTo(this GameObject go,Vector3[] path,float time,float delay,EaseType easeType){
		iTween.MoveTo(go,iTween.Hash("path",path,"time",time,"delay",delay,"easeType",easeType.ToString()));
	}
	
	/// <summary>
	/// Changes a GameObject's position over time along a supplied path.
	/// </summary>
	/// <param name="go">
	/// A <see cref="GameObject"/>
	/// </param>
	/// <param name="path">
	/// A <see cref="Transform[]"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	public static void MoveTo(this GameObject go,Transform[] path,float time,float delay,EaseType easeType){
		iTween.MoveTo(go,iTween.Hash("path",path,"time",time,"delay",delay,"easeType",easeType.ToString()));
	}
	
	/// <summary>
	/// Changes a GameObject's position over time to a supplied destination.
	/// </summary>
	/// <param name="position">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	/// <param name="loopType">
	/// A <see cref="LoopType"/>
	/// </param>
	public static void MoveTo(this GameObject go,Vector3 position,float time,float delay,EaseType easeType,LoopType loopType){
		iTween.MoveTo(go,iTween.Hash("position",position,"time",time,"delay",delay,"easeType",easeType.ToString(),"looptype",loopType.ToString()));
	}
	
	/// <summary>
	/// Changes a GameObject's position over time along a supplied path.
	/// </summary>
	/// <param name="go">
	/// A <see cref="GameObject"/>
	/// </param>
	/// <param name="path">
	/// A <see cref="Vector3[]"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	/// <param name="loopType">
	/// A <see cref="LoopType"/>
	/// </param>
	public static void MoveTo(this GameObject go,Vector3[] path,float time,float delay,EaseType easeType,LoopType loopType){
		iTween.MoveTo(go,iTween.Hash("path",path,"time",time,"delay",delay,"easeType",easeType.ToString(),"looptype",loopType.ToString()));
	}
	
	/// <summary>
	/// Changes a GameObject's position over time along a supplied path.
	/// </summary>
	/// <param name="go">
	/// A <see cref="GameObject"/>
	/// </param>
	/// <param name="path">
	/// A <see cref="Transform[]"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	/// <param name="loopType">
	/// A <see cref="LoopType"/>
	/// </param>
	public static void MoveTo(this GameObject go,Transform[] path,float time,float delay,EaseType easeType,LoopType loopType){
		iTween.MoveTo(go,iTween.Hash("path",path,"time",time,"delay",delay,"easeType",easeType.ToString(),"looptype",loopType.ToString()));
	}
	
	/// <summary>
	/// Similar to MoveTo but incredibly less expensive for usage inside the Update function or similar looping situations involving a "live" set of changing values. Does not utilize an EaseType. 
	/// </summary>
	/// <param name="position">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void MoveUpdate(this GameObject go,Vector3 position,float time){
		iTween.MoveUpdate(go,position,time);
	}
	
	/// <summary>
	/// Applies a jolt of force to a GameObject's position and wobbles it back to its initial position.
	/// </summary>
	/// <param name="amount">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void PunchPosition(this GameObject go,Vector3 amount,float time,float delay){
		iTween.PunchPosition(go,iTween.Hash("amount",amount,"time",time,"delay",delay));
	}
	
	/// <summary>
	/// Applies a jolt of force to a GameObject's rotation and wobbles it back to its initial rotation.
	/// </summary>
	/// <param name="amount">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void PunchRotation(this GameObject go,Vector3 amount,float time,float delay){
		iTween.PunchRotation(go,iTween.Hash("amount",amount,"time",time,"delay",delay));
	}
	
	/// <summary>
	/// Applies a jolt of force to a GameObject's scale and wobbles it back to its initial scale.
	/// </summary>
	/// <param name="amount">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void PunchScale(this GameObject go,Vector3 amount,float time,float delay){
		iTween.PunchScale(go,iTween.Hash("amount",amount,"time",time,"delay",delay));
	}
	
	/// <summary>
	/// Puts a GameObject on a path at the provided percentage.
	/// </summary>
	/// <param name="path">
	/// A <see cref="Transform[]"/>
	/// </param>
	/// <param name="percent">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void PutOnPath(this GameObject go, Transform[] path, float percent){
		iTween.PutOnPath(go,path,percent);	
	}
	
	/// <summary>
	/// Puts a GameObject on a path at the provided percentage.
	/// </summary>
	/// <param name="path">
	/// A <see cref="Vector3[]"/>
	/// </param>
	/// <param name="percent">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void PutOnPath(this GameObject go, Vector3[] path, float percent){
		iTween.PutOnPath(go,path,percent);	
	}
	
	/// <summary>
	/// Adds supplied Euler angles in degrees to a GameObject's rotation over time.
	/// </summary>
	/// <param name="amount">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void RotateAdd(this GameObject go,Vector3 amount,float time,float delay){
		iTween.RotateAdd(go,iTween.Hash("amount",amount,"time",time,"delay",delay));
	}
	
	/// <summary>
	/// Adds supplied Euler angles in degrees to a GameObject's rotation over time.
	/// </summary>
	/// <param name="amount">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easetype">
	/// A <see cref="EaseType"/>
	/// </param> 
	public static void RotateAdd(this GameObject go,Vector3 amount,float time,float delay,EaseType easeType){
		iTween.RotateAdd(go,iTween.Hash("amount",amount,"time",time,"delay",delay,"easeType",easeType.ToString()));
	}
	
	/// <summary>
	/// Adds supplied Euler angles in degrees to a GameObject's rotation over time.
	/// </summary>
	/// <param name="amount">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easetype">
	/// A <see cref="EaseType"/>
	/// </param> 
	/// <param name="looptype">
	/// A <see cref="LoopType"/>
	/// </param>
	public static void RotateAdd(this GameObject go,Vector3 amount,float time,float delay,EaseType easeType,LoopType loopType){
		iTween.RotateAdd(go,iTween.Hash("amount",amount,"time",time,"delay",delay,"easeType",easeType.ToString(),"looptype",loopType.ToString()));
	}
	
	/// <summary>
	/// Multiplies supplied values by 360 and rotates a GameObject by calculated amount over time.
	/// </summary>
	/// <param name="amount">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void RotateBy(this GameObject go,Vector3 amount,float time,float delay){
		iTween.RotateBy(go,iTween.Hash("amount",amount,"time",time,"delay",delay));
	}
	
	/// <summary>
	/// Multiplies supplied values by 360 and rotates a GameObject by calculated amount over time.
	/// </summary>
	/// <param name="amount">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easetype">
	/// A <see cref="EaseType"/>
	/// </param> 
	public static void RotateBy(this GameObject go,Vector3 amount,float time,float delay,EaseType easeType){
		iTween.RotateBy(go,iTween.Hash("amount",amount,"time",time,"delay",delay,"easeType",easeType.ToString()));
	}
	
	/// <summary>
	/// Multiplies supplied values by 360 and rotates a GameObject by calculated amount over time.
	/// </summary>
	/// <param name="amount">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easetype">
	/// A <see cref="EaseType"/>
	/// </param>
	/// <param name="looptype">
	/// A <see cref="LoopType"/>
	/// </param>
	public static void RotateBy(this GameObject go,Vector3 amount,float time,float delay,EaseType easeType,LoopType loopType){
		iTween.RotateBy(go,iTween.Hash("amount",amount,"time",time,"delay",delay,"easeType",easeType.ToString(),"looptype",loopType.ToString()));
	}
	
	/// <summary>
	/// Instantly changes a GameObject's Euler angles in degrees then returns it to it's starting rotation over time.
	/// </summary>
	/// <param name="rotation">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void RotateFrom(this GameObject go,Vector3 rotation,float time,float delay){
		iTween.RotateFrom(go,iTween.Hash("rotation",rotation,"time",time,"delay",delay));
	}
	
	/// <summary>
	/// Instantly changes a GameObject's Euler angles in degrees then returns it to it's starting rotation over time.
	/// </summary>
	/// <param name="amount">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	public static void RotateFrom(this GameObject go,Vector3 rotation,float time,float delay,EaseType easeType){
		iTween.RotateFrom(go,iTween.Hash("rotation",rotation,"time",time,"delay",delay,"easeType",easeType.ToString()));
	}
	
	/// <summary>
	/// Instantly changes a GameObject's Euler angles in degrees then returns it to it's starting rotation over time.
	/// </summary>
	/// <param name="rotation">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	/// <param name="loopType">
	/// A <see cref="LoopType"/>
	/// </param>
	public static void RotateFrom(this GameObject go,Vector3 rotation,float time,float delay,EaseType easeType,LoopType loopType){
		iTween.RotateFrom(go,iTween.Hash("rotation",rotation,"time",time,"delay",delay,"easeType",easeType.ToString(),"looptype",loopType.ToString()));
	}
	
	/// <summary>
	/// Rotates a GameObject to the supplied Euler angles in degrees over time.
	/// </summary>
	/// <param name="rotation">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void RotateTo(this GameObject go,Vector3 rotation,float time,float delay){
		iTween.RotateTo(go,iTween.Hash("rotation",rotation,"time",time,"delay",delay));
	}
	
	/// <summary>
	/// Rotates a GameObject to the supplied Euler angles in degrees over time.
	/// </summary>
	/// <param name="amount">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	public static void RotateTo(this GameObject go,Vector3 rotation,float time,float delay,EaseType easeType){
		iTween.RotateTo(go,iTween.Hash("rotation",rotation,"time",time,"delay",delay,"easeType",easeType.ToString()));
	}
	
	/// <summary>
	/// Rotates a GameObject to the supplied Euler angles in degrees over time.
	/// </summary>
	/// <param name="rotation">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	/// <param name="loopType">
	/// A <see cref="LoopType"/>
	/// </param>
	public static void RotateTo(this GameObject go,Vector3 rotation,float time,float delay,EaseType easeType,LoopType loopType){
		iTween.RotateTo(go,iTween.Hash("rotation",rotation,"time",time,"delay",delay,"easeType",easeType.ToString(),"looptype",loopType.ToString()));
	}
	
	/// <summary>
	/// Similar to RotateTo but incredibly less expensive for usage inside the Update function or similar looping situations involving a "live" set of changing values. Does not utilize an EaseType. 
	/// </summary>
	/// <param name="rotation">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void RotateUpdate(this GameObject go,Vector3 rotation,float time){
		iTween.RotateUpdate(go,rotation,time);
	}
	
	/// <summary>
	/// Adds to a GameObject's scale over time.
	/// </summary>
	/// <param name="amount">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void ScaleAdd(this GameObject go,Vector3 amount,float time,float delay){
		iTween.ScaleAdd(go,iTween.Hash("amount",amount,"time",time,"delay",delay));
	}
	
	/// <summary>
	/// Adds to a GameObject's scale over time.
	/// </summary>
	/// <param name="amount">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	public static void ScaleAdd(this GameObject go,Vector3 amount,float time,float delay,EaseType easeType){
		iTween.ScaleAdd(go,iTween.Hash("amount",amount,"time",time,"delay",delay,"easeType",easeType.ToString()));
	}
	
	/// <summary>
	/// Adds to a GameObject's scale over time.
	/// </summary>
	/// <param name="amount">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	/// <param name="loopType">
	/// A <see cref="LoopType"/>
	/// </param>
	public static void ScaleAdd(this GameObject go,Vector3 amount,float time,float delay,EaseType easeType,LoopType loopType){
		iTween.ScaleAdd(go,iTween.Hash("amount",amount,"time",time,"delay",delay,"easeType",easeType.ToString(),"looptype",loopType.ToString()));
	}
	
	/// <summary>
	/// Multiplies a GameObject's scale over time.
	/// </summary>
	/// <param name="amount">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void ScaleBy(this GameObject go,Vector3 amount,float time,float delay){
		iTween.ScaleBy(go,iTween.Hash("amount",amount,"time",time,"delay",delay));
	}
	
	/// <summary>
	/// /// <summary>
	/// Multiplies a GameObject's scale over time.
	/// </summary>
	/// <param name="amount">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	public static void ScaleBy(this GameObject go,Vector3 amount,float time,float delay,EaseType easeType){
		iTween.ScaleBy(go,iTween.Hash("amount",amount,"time",time,"delay",delay,"easeType",easeType.ToString()));
	}
	
	/// <summary>
	/// Multiplies a GameObject's scale over time.
	/// </summary>
	/// <param name="amount">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	/// <param name="loopType">
	/// A <see cref="LoopType"/>
	/// </param>
	public static void ScaleBy(this GameObject go,Vector3 amount,float time,float delay,EaseType easeType,LoopType loopType){
		iTween.ScaleBy(go,iTween.Hash("amount",amount,"time",time,"delay",delay,"easeType",easeType.ToString(),"looptype",loopType.ToString()));
	}
	
	public static void ScaleFrom(this GameObject go,Vector3 scale,float time,float delay){
		iTween.ScaleFrom(go,iTween.Hash("scale",scale,"time",time,"delay",delay));
	}
	
	/// <summary>
	/// Instantly changes a GameObject's scale then returns it to it's starting scale over time.
	/// </summary>
	/// <param name="scale">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	public static void ScaleFrom(this GameObject go,Vector3 scale,float time,float delay,EaseType easeType){
		iTween.ScaleFrom(go,iTween.Hash("scale",scale,"time",time,"delay",delay,"easeType",easeType.ToString()));
	}
	
	/// <summary>
	/// Instantly changes a GameObject's scale then returns it to it's starting scale over time.
	/// </summary>
	/// <param name="scale">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	/// <param name="loopType">
	/// A <see cref="LoopType"/>
	/// </param>
	public static void ScaleFrom(this GameObject go,Vector3 scale,float time,float delay,EaseType easeType,LoopType loopType){
		iTween.ScaleFrom(go,iTween.Hash("scale",scale,"time",time,"delay",delay,"easeType",easeType.ToString(),"looptype",loopType.ToString()));
	}
	
	/// <summary>
	/// Changes a GameObject's scale over time.
	/// </summary>
	/// <param name="scale">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void ScaleTo(this GameObject go,Vector3 scale,float time,float delay){
		iTween.ScaleTo(go,iTween.Hash("scale",scale,"time",time,"delay",delay));
	}
	
	/// <summary>
	/// Changes a GameObject's scale over time.
	/// </summary>
	/// <param name="scale">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	public static void ScaleTo(this GameObject go,Vector3 scale,float time,float delay,EaseType easeType){
		iTween.ScaleTo(go,iTween.Hash("scale",scale,"time",time,"delay",delay,"easeType",easeType.ToString()));
	}
	
	/// <summary>
	/// Changes a GameObject's scale over time.
	/// </summary>
	/// <param name="scale">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="easeType">
	/// A <see cref="EaseType"/>
	/// </param>
	/// <param name="loopType">
	/// A <see cref="LoopType"/>
	/// </param>
	public static void ScaleTo(this GameObject go,Vector3 scale,float time,float delay,EaseType easeType,LoopType loopType){
		iTween.ScaleTo(go,iTween.Hash("scale",scale,"time",time,"delay",delay,"easeType",easeType.ToString(),"looptype",loopType.ToString()));
	}
	
	/// <summary>
	/// Similar to ScaleTo but incredibly less expensive for usage inside the Update function or similar looping situations involving a "live" set of changing values. Does not utilize an EaseType.
	/// </summary>
	/// <param name="scale">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void ScaleUpdate(this GameObject go,Vector3 scale,float time){
		iTween.ScaleUpdate(go,scale,time);
	}
	
	/// <summary>
	/// Randomly shakes a GameObject's position by a diminishing amount over time.
	/// </summary>
	/// <param name="amount">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void ShakePosition(this GameObject go,Vector3 amount,float time,float delay){
		iTween.ShakePosition(go,iTween.Hash("amount",amount,"time",time,"delay",delay));
	}
	
	/// <summary>
	/// Randomly shakes a GameObject's rotation by a diminishing amount over time.
	/// </summary>
	/// <param name="amount">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void ShakeRotation(this GameObject go,Vector3 amount,float time,float delay){
		iTween.ShakeRotation(go,iTween.Hash("amount",amount,"time",time,"delay",delay));
	}
	
	/// <summary>
	/// Randomly shakes a GameObject's scale by a diminishing amount over time.
	/// </summary>
	/// <param name="go">
	/// A <see cref="GameObject"/>
	/// </param>
	/// <param name="amount">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="time">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void ShakeScale(this GameObject go,Vector3 amount,float time,float delay){
		iTween.ShakeScale(go,iTween.Hash("amount",amount,"time",time,"delay",delay));
	}
	
	/// <summary>
	/// Plays an AudioClip once based on supplied volume and pitch and following any delay. AudioSource is optional as iTween will provide one.
	/// </summary>
	/// <param name="audioClip">
	/// A <see cref="AudioClip"/>
	/// </param>
	/// <param name="volume">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="pitch">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="delay">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void Stab(this GameObject go,AudioClip audioClip, float volume, float pitch, float delay){
		iTween.Stab(go, iTween.Hash("audioClip",audioClip,"volume",volume,"pitch",pitch,"delay",delay));
	}
}
