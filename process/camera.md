# Actual games to develop

---

# Experiments

## To do

- _Director's Cut_ (switching aesthetically between cameras while some else/AI plays the game)
- _Out of frame_
- _Camera shells_ (The single camera is oriented relative to the most recently fired shot, so there's a directorial action in shooting)
- _My life as a shell_ (you are a shell with no control, start in darkness (or perhaps see a circle of light (the end of the barrel) then get fired out of it at some point by an AI tank you're associated with, then explode and are gone)
- _Selfie Stick_ (unclear, but a funny camera angle? Navigation by selfie?)
- _Two in a tank_ (split screen two player from inside a tank, one can steer, one can see through a slit as per real tank)
- _War photographer_ (running around AI game taking pictures to document the horrors of war)
- _Collateral damage_ (First person inside a house, maybe can't leave? Tanks maybe target you or your house?)
- _Recording_ (Investigate the ability to record and play back video, or even simulation?)
- _Crime-watch_ (Define certain behaviours as illegal, player can report on tanks that break the rules - a form of surveillance)
- _La Jetée_ (can the game be presented as a series of stills?)
- _World space camera display?_
- _Dynamic skybox?_ (possible to create my own weird thing?)
- _Remember the dead_ (add cameras that display the view of every tank that's died?)




## Tried

- ~~_Don't Clear_~~ (just don't do it, man)

As I wrote in the commit message, this does lead to quite beautiful effects. It has a relationship to Julian Oliver's work with the Quake engine and not clearing to create a kind of painting relationship. I find that the Tanks! version of it is more immediate because you're actively trying to decipher the action/nature of the space. As much as I tried that, though, I have to admit there were plenty of times where I just didn't really understand the situation being represented whatsoever, which was kind of pleasant? It looks pretty incredible. It's a bit of a throw-away gesture in its simplicity (but so was Ghost Pong), but it yields surprising depth, I think, in terms of ideas about physics, clarity, artistic representation, etc...

- ~~_Shell cuts_~~ (camera cuts to aim back along the vector of the last fired shell?)

I've only got a very basic version of this working (after significant confusion about how to figure out the 'reverse angle' bit of it - got there eventually by just using Euler angles and subtracting 180 from y). I also only have a single-player approach right now, so that you just drive around a set the camera to constantly face you. I quite like the sense of vanity that it invokes - the tank perpetually "seeing how it looks" in different settings? That idea of posing is quite nice, and it's emphasised by having control of the camera which is normally so austere and removed OR is just literally your eyes. Moving a camera _as a camera_ is a real act of power as a player I think?

- ~~_Surveillance_~~

Very obvious after doing the split screen in the sense that it's just a bunch of cameras (16) in the scene which map to specific locations in the viewport so that they form the traditional array. It would probably be nice to put some kind of filter over them for the CCTV experience? There are lots of aesthetic connotations to play with. There's the question of whether the cameras themselves should be visible in the scene? There's the question of what "more" you should do. But certainly the experience itself was pleasing as per the commit: seeing yourself passing from one camera to another, or seeing yourself in multiple cameras at the same time. Studying the wider angle, more distant cameras to find yourself as a little dot was satisfying too. It's obviously a pretty different game - not very playable, but somehow quite evocative of all the obvious ideas about surveillance, being watch, being suspected of bad behaviour (well you are firing rockets all over the place!)

- ~~_Split screen first person_~~

Implemented this rather straightforwardly. Viewing the world in first person clearly introduces _so many possibilities_. And indeed this is probably generalisable to cameras as a whole - the way you perceive space has an enormous impact on everything about the game. First person felt much more intimate and immediate and you can see detail, of course, but then also it becomes possible to hide, for example. It was an exciting first step.

---

# Design journal

## 2018-01-07 13:39, in which I write a couple of words about starting on a couple of camera experiments

Given that the first prototype challenge for CART 415 is not just Light but also Camera, it makes sense to acquaint myself a bit with thinking about that aspect of things as well. So I at least popped together the spit screen view just to have had at least a little experience there. It was super easy and, as above in the (experiments)[#experiments] section, it felt really generative and powerful to see the world from that new perspective, quite exciting. So I foresee working with Camera being quite a fun project here and I can already thinking of a number of ideas. I should do a formal brainstorm, but

- The idea of _Cinematic cutting/editing_ has a lot of potential, both in terms of player-controlled cuts (whether for aesthetics, as a 'power', or for some other reason), CPU controlled cuts (again, could be aesthetic, could be something else)
- The idea of _Surveillance_ is clearly powerful. I'm particularly drawn to the aesthetics of surveillance - static shot, grainy, not necessarily helpful in the context of being someone in the scene moving around. Again you could imagine being the surveillor or the surveilled.
- The idea of things being _Out of the frame_ and therefore unseen but still existing/present. Like the idea that things could be taking place you can't see.
- The idea of _Shooting camera_ (funny reversal of words there too actually) so that you actually fire the camera (yourself if we're talking FPS) in order to move, rather than keys to move

The camera thing (and the desire to consider a version of the game where you're _not_ the tank) also leads me to want to implement some AI for the game so that I can have tanks doing their own thing in the scene separate from the player, who can then act at a meta level. I found a tutorial online that looks feasible so I'll go through that today or tomorrow for fun.

## 2018-01-18 11:07, in which I brainstorm Camera a little more directly as I did with Light earlier

__What is the nature of the Unity Camera?__

It's a component added to a game object technically (like a Light), but most of the time that would be an empty game object as I understand it. So it's like an object on its own. It is what "looks at" a scene and represents it on the user/player's screen. There can be more than one. It has specific qualities as reflected in its editor panel:

- _Transform_. Like any other object. Moving the transform obviously transforms what the camera can see - it's like moving around a physical camera in that sense. You can translate and rotate it. This allows for specific "camera setups" that you might think of in terms of cinema. A camera can be in a specific world location at a specific location.
- _Clear Flags_. This concerns what the camera sees when there is no intervening object in the scene. The default is a skybox. But there are options:
  - _Skybox_ tries to represent the idea that it's basically "space" everywhere that's not the objects in the scene. The default is a dynamic skybox that is affected by the rotation of the scene's directional light. I guess this puts the concept of a skybox into the domain of the Camera actually, which is an interesting implication of this process.
  - _Solid Color_ replaces everything with one colour. It's quite satisfyingly flat.
  - _Depth Only_ has an example on the Unity manual which is that you might use it to draw a weapon with a separate camera overlaid on the main camera, and Depth Only would mean it would never clip through anything. I guess it eliminates clipping? If I turn it on for Tanks I see... actually quite a satisfying continuation of the edges of the objects at the edge of the level? Not clear what that "means". The Unity example raises the perspective that you can overlay multiple cameras that render separate objects differently, which could be interesting, particularly for drawing attention to artificiality? (I guess this involves the Culling Mask too)
  - _Don't Clear_ basically leads to glitchiness where the frames draw over each other which sounds fun. When I enable that I see... an incredibly beautiful trippy mess! Wow, that's a version right there? _Don't Clear_ seems like pretty adequate all on its own. May be better served with a fixed camera pulled back?
- _Culling Mask_ determines what the camera sees... fucking with it in Tanks seems to yield nothing interesting. It's about "selective rendering" but I couldn't seem to get that to have any impact. It may be an "issue" with how the layers are being setup in the game... something to play with.
- _Projection_ is either Perspective or Orthographic. Tanks! is Orthographic and that's pretty. You need perspective for first person cameras I believe? There's lots of shit about the frustum to deal with in this context. It's maybe surprisingly uninteresting overall? Maybe it's worth thinking about first person orthographic cameras?
  - _Size_ - this applies to orthgraphic - refers to the area of the camera's frustum I believe. The larger the number the more of the scene you see at once.
  - _Field of View_ - this applies to perspective. Default is 60º. As far as I can see it's the angle out from the camera? Making it large is intriguingly bizarre. Worth a play to set it to 179 or something with a camera closer to the ground?
- _Clipping Planes_ - Divided into Near and Far. Determines the closest something can be to the camera and the furthest before it's not rendered. Defaults appear to be 0.3 and 1000. Allows for a kind of beautiful darkness if you bring the Far plane in. Kind of fun.
- _Viewport Rect_ - Determines the dimensions and location that this camera is displayed on the screen. The most obvious thing it does is allow for the display of multiple cameras on the screen. Hence _Split Screen_ and _Surveillance_ being the core two things. (This makes me wonder about _world-space camera display_? Maybe that has to be a texture thing?)
- _Depth_ - This is where in the stack of multiple cameras this one would draw, a priority thing for overlapping cameras.
- _Rendering Path_ - I mean, I know it's important, not clear I want to mess with it? Deferred seems the most privileged for its treatment of shadows/light, but this is mostly a performance thing - e.g. you might need something less hardcore for mobile.
- _Occlusion Culling_ - Should things be culled from the camera if they're not visible? Probably largely a performance thing too? I should read a little more to see if there are fun ways to play with it.
- _Allow HDR_ - It's off for Tanks. When it's on it has no impact. Should probably learn a bit more?
- _Allow MSAA_ - It's on for Tanks. It's something to do with antialiasing. It's very confusing and sounds like it's disabled for deferred rendering? Maybe something to do with trying to make less powerful rendering computers look a bit less jaggedy?
- _Target Display_ - I guess this is for multiple display games. Not the case for this project.

__The Camera API?__

- "A Camera is a device through which the player views the world."
- There's a lot going on in the API, quite heavy duty (I suppose because the Camera is such a fundamental aspect of the system)
- Obviously a huge amount of it is programmable references to the various qualities in the editor pane above, but there's significantly more
- There's a preponderance of conversion between the concept of Screen and World and Screen and Viewport (because the Viewport is normalised). I suppose there's something in there about the ability to "click on" 3D objects for example?
- Idea like "Get All Cameras" are kind of pleasant... the idea of manipulating multiple perceptions of the scene at the same time?

__Tanks!'s relationship to camera?__

- It has a specific script written to maintain all tanks within the field of view of the camera at all times. As discussed in 415 this prioritises the idea of tank-on-tank action as the central concern (rather than the landscape for example). The camera zooms as needed to maintain that view. Points out that you can manipulate the camera's properties in relationship to some aspect of the game state (no reason you couldn't switch cameras on fireing for example - what if you moved the dominant camera to face the negation of the .forward of the shell after impact? Perpetually cutting to look back to where a shell came from? _Shell cuts_)
- The camera also has post processing effects applied to it in the default tutorial? But I think I disabled them by moving away from the baked/generated lightmap? There's a whole world of consideration when you start thinking about this.
- This postprocessing thing makes me wonder if _shaders_ are therefore a consideration in the domain of the camera? Given that shaders determine how pixels should be _rendered_, and that rendering exists relative to cameras pointed into the scene?
- The camera in the game is on a 'rig' to make it more manipulable? A second order kind of thing?
- Obviously the game uses an orthographic camera specifically - I suppose there's some extent to which the game's assets were created with this in mind then? There's are specific settings for the frustum to make this work.
- The camera is ultimately about providing all players with total information. A matter of fairness, balance, a specific epistemology of the geography. ETC.

__Qualities of the game in relation to Camera?__

__Movement__: Movement drives where the camera positions itself specifically, so there's an idea of _controlling_ the camera with the relative position of the tanks, a weird way to control something like that, but interesting? Indirect control.

__Shooting__: The camera relates to shooting in terms of the obvious informational qualities the "fair" camera provides. You can always see your opponent (pretty sure there's "nowhere to hide"?).

__Evaluation__: This is really the heart of the justification for the way this camera is I suppose? It's about providing exactly the same evaluative possibilities for every player (well every human player - the AI's don't operate based on anything to do with the camera). An FP camera, for example, offers asymmetric information depending on where you position the camera-that-is-you.

__Cultural references implied by Camera?__

__Cinema__: I mean, this is kind of 'too big', but the base level point here has to be that a core use of cameras and the meaning of the camera comes from cinema. There's going to be millions of cinematic techniques and cultural touchstones, though, so it's not really possible to "rely" on this? Like, do you want Goddardian cuts? Eisenstein's montage theory? Haneke's fourth-wall-breaking? The first person camera of Enter the Void? Thinking about more conventional camera techniques like reverse shots, close ups, establishing shots? All of these are entirely possible... the whole cinematic language is available in all its glorious conflict with a realtime combat game? There's rather a lot to think about. But at the very least _cutting_, _cinematic shot compositions_, _directorial styles_, ... the _camera in motion_, the _completely static camera_, _off screen action_, _point of view shot_ (FPS? Whose point of view?)

__Photography__: The other most clear use of camera is photography. Photography is more or less about capturing a still frame. This makes me think of _La Jetée_ though - the cinematic use of photographs. Stillness is quite beautiful. Can you make the La Jetée of tanks? Photography implies _Selfies_ and also _Family Photos_ and other experiences of photography beyong being a photographer.

__Using a camera__: Related to the above but in a sense inverted - the interactivity offered can be about using and manipulating a camera rather than solely being observed by it. The act of framing, selecting, recording are beautiful artistic things a player could do. Reminds me of the poetry game I wanted to make where you find accompanying images as a photographer. Obviously _war photographer_ fits into this element.

__Surveillance__: Already addressed this a little in an experiment, but it's a powerful thing to be surveilled. It's interesting the degree to which the game is _already_ a matter of surveillance (though the fact the camera facilitates the game belies that a bit). _Reporting crimes_ becomes a possibility in that context.

__Watching__: If there is a camera recording it becomes possible to watch what it records (live or after the fact). _Watching recordings_ is a powerful thing we do. Spectatorship.
