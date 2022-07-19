# This document

This is a design journal/working space for games in __TANKS!ES: Light__. The idea will be for it to serve as a design reflection journal (at the bottom, chronologically from top to down), and then also as a specifications/TODO area where I can write about the individual variations and what they need in order to be actually implemented. I think this makes sense. If it does, I'll do something else.

---

# Actual games to develop

- _Drive Into the Light_
- _Shadows Only_ (Shadows can't harm each other?)

---

# Experiments

## To do

- _Refractions_ (Could be a way of distorting play if it's viewed through water, maybe the tanks float upward in the water and eventually stop working?)
- _Silent Hill_ (Not sure this makes sense anymore)
- _A Little Privacy Please_ (This one probably makes sense? Pretty borderline in terms of connection to Light though?)
- _Dancing the Moonlight_ (This one makes sense to me)
- _In Memoriam_ (What is this?)
- _The Lion Tamer_ (I don't know if this makes sense)
- _The Fog of War_ (Redo this with the idea of either increasing fog or just totally illegible scene)

## Tried

- ~~_Do Not Go Gentle_~~

Basic version in which you have the text overlaid on the scene (in black arial for now). The light dims with each progressive line. Staccato, with lines every 5 seconds for the moment. May be better for the light to Lerp down rather than jump. There's something here in terms of the feeling of dimness against the power of the poem. The tanks need to have more of a relationship to it. Feels like they could move slower with the dying of the light? A feeling of loss of power? Which the player might then rage against and shoot their gun against?

- ~~_Strobe_~~

There's something pleasingly uncanny involved in the movement of the shadows in this piece (directional rotation is randomly altered each flash). Wonder if this combined with a fog would start to get somewhere interesting? (Also by the way wondering about Fog in first person with coloured fog? But you can only have a single colour fog right? But even then it could be interesting - more interest? In FP?)

- ~~_The Fog of War_~~

Underwhelming on first approximation, but also aesthetically quite beautiful. May make more sense as a layer in some other thing, notably in _Silent Hill_ which has that concept pretty strongly.

- ~~_No Light, Just a UI and Particle Effects_~~

Actually kind of satisfying. Some decisions to make around whether the meshes are left visible or not, and of course whether the overall environment is left visible? I guess there's an argument to make for the idea that it's the UI that casts the light specifically? (Approximate with a coloured point light according to the colour of the health bar perhaps? What about the arrow?) I qutie like that conceptually... might be more possible to navigate the terrain? Alternative is _no_ obstacles which may be quite underwhelming?

- ~~_Reflections_~~

I put a water level in and it looks pretty with things half underwater and the tanks roaming through. But also not especially interesting and also doesn't feel much of a connection to _light_ specifically. Might be possible to use this some way? Even the concept of playing the game in its reflection isn't actually _that_ interesting?

- ~~_Vampire Tank_~~

Most recently implemented. Works mechanically so I can at the very least pursue the idea from here. A tank that knows when it's illuminated by the sun. Go from there. Whether it's a horror affair about a vampire tank, whether it's to do with the _Silent Hill_ level somehow, whether it's about getting a tan, being a cat looking for a sun patch, or something else? Could fold into the sex game too - have to find shadows to fuck in?

- ~~_Light Shells_~~

Written about this below. Kind of underwhelming, even though the idea of the players _adding light_ to the environment has an appeal. Player-controlled light is kind of absent thus far. Particle trails?

- ~~_Shadows Only_~~

Written about this below. Quite beautiful. Works as an aesthetic experience. I like the idea of asking what the shadow of music is etc.?

- ~~_Day and Night_~~

As a base concept it obviously works. Underwhelming on its own, but it may be that it fits into some other ideas.

- ~~_Walk into the light_~~

This works surprisingly effectively, worth expanding into something fuller. Can do a better job of modelling the tunnel I assume. Integrating it into the main play is important I think for the contrast effect? Otherwise it doesn't make as much sense... if the tank's already dead at the beginning?

- ~~_Headlights_~~

Useful basic function, it works, may get used in _Silent Hill_. First person probably more interesting. Scary shadows.

- ~~_Hyperfunctional Lighting_~~

Surprisingly failed given this seemed one of the most theoretically sound ideas I'd had? May be that I should approach this again a couple of times?

---

# Design journal

## 2018-01-03 ??:?? Day and Night, Play and Sleep

In commit 2c13c2d0322a8879b38de5470d3ab40fe0d753c3 I created a version in which the sun (directional light) rotates and therefore creates a cycle of day and night in the world. Shadows length, in turns to night, there's moonlight, then the sun comes up again and on we go. It was boring in the immediate term, but it made me think about rhythms of life that take place in the day versus at night. In particular it started to make me think of the idea that at night the tanks would go home. And in fact I naturally thought about the tanks as _children_ who have to go home (or they'll get in trouble with their parents). They can go out and play again the next morning.

Also still possible to think more generally about the implications of day and night as human concerns and how that might be translated to the world of the game. For instance it cold be the tanks' _job_ to engage in combat, making money for their family, etc. Just concerned by being overelaborate and not having the time to really make these things.

I like this because
- it trivialises the actual combat by framing it in something implied to be larger.
- The tanks being children makes their play inconsequential in a really final-feeling manner.
- It's like the game is double-infantilised, which feels like it removes more of the sting?

What would I need?
- Need houses for the tanks to go to. Really suburban houses would be funny. Maybe they live next door to each other.
- Need to inform the player that they have to go home. Could be direct text with a timer. Could be a voice from the houses calling the children back in ("Red" and "Blue"?)
- I guess you could ask whether the tanks should even have health in this scenario? Should they "die" or should they just play on until they have to go home?

Issues...
- Does this stray too far from the idea of a conversation with light? Is it overwrought? Is it too complex while simultaneously not being all that interesting? Very narrative, not very formal? More like a free association with a specific property of light?


## 2018-01-03 ??:?? Light Shells

I'm implemented the basics of this in commit 40a69e23c165aae27ed3a7402f717466c1504c47 (just now noticed these are not being auto converted to commit links which is sad, renders them kind of stupid, oh well). The idea here is that you replace the projectile itself with a light and instead of exploding it just rests on the environment itself, therefore adding to the overall light of the scene.

I've had ideas around this become a new style of the tanks fighting - they fight with light, where going into the light reduces health etc. Or alternatively the opposite - going into the light is how you score points, or something. But again this feels like really trivial intervention, just recasting one typical game element (a 3D model of a shell with a rigidbody) with another (a light source). Yeah it's aesthetically different, but not _that much_.

I need to think more deeply than this about what light does and try to work from there? Or is _TANKS!_ overwhelming in its kind of rhetoric and world view? This will need some thought.


## 2018-01-05 10:31 Shadows Only and Walk Into the Light

(This are actually already in PROCESS, but I want them here too for completeness because this is where I think I should have been reflecting on the specifics of those designs? It's tricky because they also involve reflection on The Method too and the conversation with materials. But I think I need to split that into individual games/collections or the PROCESS journal is going to be insane?)

__Shadows Only__. Just uses a specific Unity Light setting (the eponymous Shadows only on the renderer) to turn the entire game into its shadow. It ended up looking quite beautiful. I ended up setting it on a grey plane as well to get rid of any implications of a 'real world' - pure shadow. There are potential further aesthetic choices to be made here - notably what sound should be like in a shadow world, and whether it makes sense to have the same basic gameplay announcements (or rather the shadows of them?). There are also potential gameplay-oriented decisions that emerge from this. Such as emphasising the idea of _hiding_ in shadows (as a shadow) which becomes possible but would require a different way of seeing the world (and frankly the interaction between camera and shadows is interesting - a shadow world seen in first person might be quite powerful?). There's also a gameplay implication in _moving the light_ such that the shadows change over time. A day/night cycle could end up kind of intriguing - there are times you're invisible, times you're very precise (noon) and so on. So it's a generate idea. I think it makes the list of Light games I'll actually implement. Not bad for a small amount of work, and all stemming from a single technical setting.

__Walk Into the Light__. The prototype remodels the entire scene from scratch. It creates a tunnel of light for a single tank to drive through, emulating that idea of going into the light after death - it's one of the major cultural signifiedr of 'light', a case where 'light' is used as is to have major implications. One nice thing about this prototype other than the actual representation of the idea (which broadly works I think) is that it's still constructed out of the materials of TANKS!. You have the tank of course, the camera rig (sans controller), the tunnel is made out of the "cliffs" that surround the original level rotated and scales. And a spotlight is put at one end, the camera shooting down the tunnel. The clear flags is white so that at the end of the tunnel there's just white and not a skybox or a colour. Pretty effective for rapid prototyping. Like shadows only, the base concept has various implications. There's a process/aesthetic implication around how you construct scenes that aren't from the original game - e.g. are you "required" to use the pieces that TANKS! provides? I quite like that conceptually... still using those materials. And then also larger gameplay implications - as opposed to a single isolated scene you could imagine integrating this 'walk into the light' mechanic into the original game such that when a tank is destroyed it gets this experience, and the remaining tank is left along (for all time or until suicide). As such I think this is another game I'll be putting in.


## 2018-01-05 10:33 In which I do a bit of a proper brainstorm through design possibilities again

__What are the types of light according to Unity?__

__Point light__ A point light defines a spherical area with fall off. You can match the area with a spherical collider, and so you could know whether an object is illuminated by the light and, theoretically, by how much (if you understand the falloff equation?). So there's definitely a sense of "collision" with these lights available. I tried to explore that a bit with the _Light Shells_ version... the lights behave like objects that can be fired. I didn't get onto the collision element because I found the base idea pretty uninspiring. I'm going to guess there will be technical penalties for firing too much light as well? It occurs to me there's the possibility of the tanks _being_ light and firing light that diminishes them (you can fire an amount of yourself to damage your opponent, or you can go and collect it back). The more you fire the dimmer you get and the easier it becomes to hide? You could hide in patches of your own light? The lights would presumably need to be different colours.

__Point light 2__ Point lights are also used to evoke various kinds of domestic or gentle lighting. Lightbulbs for example. There a potential cosiness to the point light.

__Spotlight__ A spot light projects a circle of light onto surfaces. I guess it has falloff toward its edges? It has an angle and a range and an intensity. Less straightforward to calculate collisions. Very possible to _aim_ it at something to illuminate it. Spotlights naturally evoke the theatre, the idea of highlighting the thing you should watch. _Hyper Function Lighting_ attempted something like this with spotlights on absolutely everything but it didn't feel very successful. The joke is lost in the complexity of the scene. Another connotation of spotlights is surveillance and security, prisons. Being followed by a spotlight means you can't get away, you're being tracked and hunted. So a spotlight can be desirable (stage) and undesirable (hunting, surveillance). Spotlights can be used at a small scale (e.g. torch) or much larger. I made the _Headlights_ version in which the tanks have headlights. It was boring but the dynamic shadows were quite beautiful in that environment. Evokes the horror genre obviously. (Light and horror go together.)

__Directional Light__ A directional light is effectively the sun. Affects everything everywhere equally (though it respects internal spaces). It interacts with Unity's skybox to imply a sun in the sky at a particular time. As you rotate the directional light you're changing the angle of the sun and therefore the time of day. There even seems to be a moonlit illumination when the sun has set. There's therefore a strong connection to _time_ with this light. The simple _Day/Night_ version I made was an attempt at this, but it doesn't feel like much of anything, I guess because the time gives no real context or consequence to play. Clearly the changing of the sun affects play - it can get lighter or darker, the shadows move. I wondered whether I should manipulate this light in _Shadows Only_ to create more dynamic play. Shadows feel really important here.

__Area Light__ I've never really used an Area Light and it's my understanding they're only for Baked lighting setups. They seem _expensive_. That in itself is a major proposition with lighting in general: it's expensive. Robert Yang's point about how many FPS games were devoting just to _shadows_ is pretty incredible. Especially as someone inexperienced with light and efficiency in Unity code, it feels dangerous to push too hard. That said making a point about _performance_ is not out of the question? The idea that to have enough light time slows down? (There was that vague concept in _v r 3_, where certain water effects were lagging the overall framerate... a kind of "true physics" of the game engine?)

__Reflection Probe__ These are used to capture reflections in an environment. They used by things with reflective materials (like water or shiny metals). Those materials use the closest probe to determine what reflection they should show. As such this will obviously be relevant to anything I do with _water_, but also raises the point of reflective materials more generally - what meaning could be found in mirror or near mirror surfaces in the context of tanks?

__Light Probe Group__ I'm not 100% sure these matter with Realtime lighting? Presumably these apply more to things like emissive materials and area lights? I should keep these in mind but they also seem like they might be a technical step too far for me personally to really engage with. (And therefore a reason to be a bit grateful I'm not just doing one game per lighting concept in Unity.)

__Ambient__ Not a light you create in the scene but a generically present form of light. Illuminates literally everything. Obviously can create mood through brightness/dimness and colour tints. A scene lit only in Ambient light has a really beautiful _flatness_ to it because of the lack of shadows. Could imagine messing with depth perception (perhaps especially with an orthogonal camera as this game has by default?). There's something to be said for the simplicity of ambient only with the light fluctuating (say between lighter and darker red, which I just tried in editor). It has a feeling to it for sure. Complete silence? No sound?


__What are entities related to Light according to Unity?__

__Water__ It occurred to me in the bath last night (how appropriate) that _water_ is totally connected with light in Unity through the two ideas of _reflection_ and _refraction_. Both of those could plausibly be used. Both are about a distorted/unusual way of perceiving the scene because they alter the behaviour of light. Both by inverting it (reflection) and distorting it (both, via waves). There's a huge amount of flexibility in the default Unity water in terms of how wavy, how reflective, how refractive, what colour, etc. It should be possible to imagine something? At the very least I should take a look at whether I could have the game played in a reflection? (Can water be upside down and reflect what's above it?)

__Shadow__ Shadows are a key consequence of light. Obvi. Shadow has implications, mostly negative. Hiding. Darkness and evil. Lurking. Obscurity. Inability to perceive details (and therefore threats). The time getting late. Shadows are also very beautiful (in Unity and therefore in the real world - or is that the other way around?). _Shadows Only_ is a nice way to focus in on how beautiful shadows are. (I also wonder whether another version of this would be to replace all materials with completely plane texture (white?) and then have space defined by shadow? Different to Shadows Only, though subtle. Because a thing we miss is shadows projected onto non-ground surfaces in that version.) _Death-by-shadow_ is an obvious concept by I don't believe it can make sense? It might be possible to raycast in such a way as to determin whether a tank is in light or shadow? If there's only a directional light and you fire a ray from say the tank's centrepoint in the backwards direction of the directional light and you don't hit anything then there's a clear line of sight from the light to the tank('s centre) and you could do something with that information. That evokes a vampire narrative of tanks scurrying between the shadows which is appealing (especially as the time of day changes?) _Vampire tank_? What would a vampire tank eat? Normal tanks I suppose. It can't be killed by conventional means, only by the light (you have to force it into the light?) or by some sort of religious/totem/stake/thing which the other player(s) could pick up? That's sounding _very_ gamelike. Also vaguely complex? Also like it could just _not work_. Is it possible to turn it into more of a single-player narrative game about a vampire tank? Make it like Nosferatu? Camera effects and interstitials about the Tank. Maybe it's trying to get somewhere?

__Fog__ Haven't given much thought at all to fog, but it's very obviously a thing that affects light (through visibility). Immediately it evokes the _Fog of war_ which seems like a potential subject for messing with the metaphor there? A completely dense, soupy fog? An _Olafur Eliasson Fog_? Stephen King's "The Mist"? Fog that dynamically grows and shrinks during play? A dangerous time to be out in the fog? The most powerful ideas there, to me, seem to be the potential of the metaphorical fog of war and how it could be used as a metaphor for something else? Trauma? PTSD? Worth thought.

__Materials__ As above, reflectivity is a property of materials, and so is _emission_. After all, the materials on the 3D objects are what determine how it reacts to light. Emission is a pretty fun property (another one of those things that only works with baked lighting though? So maybe off the table. No it's available in Precomputed Realtime GI, so it's plauisble if I want to have an emissive illumination setting. Could generate the lighting for just that one scene?) Even in the absence of precomputed GI emissive materials are intriguing because they self-illuminate. So they can show up in the dark etc. (even if not casting light onto other things). More generally just things like albedos, how shiny or dull things are, are all related to the quality of light. (I do need to think somewhere though about not just creating versions of the game that are effectively monotonous tech demos of specific Unity features. _Shadows Only_ is at vague risk of that even.)


__What is TANKS!'s relationship to Light?__

I think I've already written about this to some extent somewhere? It's an important discussion to have with the class (just a side note to myself, even though I already knew that).

TANKS! has a purely(?) _functional_ idea of light. Well, functional with an acknowledgement of wanting something that looks to some extent beautiful too. But you know if something had to be sacrificed it would be the aesthetics, right? So the scene is "well lit" in the sense that everything is clearly visible. There's ambient light so that there's no such thing as a black shadow. There's directional light that casts attractive shadows and adds most of the illumination to the scene. The materials are all bright. (I suppose you could even think of the UI health circle as a form of light if you wanted to. And actually with absolutely NO light you'd end up with a UI-only version of the game which would be pretty funny... the things that don't need external illumination? Should make that. _No Light_. All obstacles full emissive? Because it's dumb to now be able to see them?)

So TANKS! is all about light that helps players to play the game. They can see each other. They can see the shells fired (because they have an additional point light to make absolutely sure of that). The can see the explosions, the UI, the terrain (and therefore navigation possibilities). Etc.


__What are the qualities of TANKS! the game and how can they relate to Light?__

__Movement: Seeing Where You're Going__. It crucially involves being able to see where you're going. It's not _that_ interesting to not be able to see where you're going in the context of this game. That kind of thing is interesting, perhaps, if you're under threat from an unseen enemy (and not so much an enemy who also can't see where they're going?). It becomes more interesting in first person perhaps? The torches version becomes a kind of horror experience if you're both navigating around in the dark with torches trying to kill each other? A _Silent Hill_ version then? Complete with radio static that gets louder as you get closer to each other? Slower movement? Assymmetric roles where one is Pyramid head and one is vulnerable and trying to run away? Don't even necessarily need to see yourself as a tank (except I guess you do when you see the other - though that's kind of comedic, so maybe worthwhile?)

__Movement: As Resource__. It's not a factor in tanks because there's no fuel and no way in which the quality of your movement changes, but movement could be transformed into a resource. It's conceivable you could move differently in light and in shadow? (This starts to stray into _Vampire tank_?)

__Movement: As fleeing__. In this game there's no specific reason to be "afraid" of the enemy because you are exactly equal in ability. There's no real point of running away, but rather potential advantages in terms of the spatial layout? The assymmetric version of _Silent Hill_ above would be one way to think about different relationships to movement in the space.

__Movement: As finding somewhere private, Shooting: As fucking__. If the tanks wanted to have a romantic interlude or sexual intercourse (perhaps by excitedly shooting each other in the face?) then they would need to find privacy, away from the light. A spotlight might follow them around. How can they find that intimate space? "You were caught fucking!" (This obviously reminds me of Lea's _Ute_.) You were caught fucking. Ha ha ha. By a censorious tank. Hehehehe. Oh boy. No I think if it's another tank it might be too much? Plus it sounds like AI nightmare? Maybe not. _A Little Privacy_. The tanks' meters fill _up_ as they're satisfied sexually, and perhaps diminish to encourage a frenzy and thus colocation - but you could still "fuck at a distance" from two sets of shadows? Ha ha ha. Oh god. (Tech note: for a cone-shaped collider in the case of detecting illumination by a spotlight you create a cone mesh and use that as a mesh collider.) (Culture note: I'm thinking here to some extent of being in India and that idea of illicit couples gathering to kiss in dark garden corners.)

__Movement: As dance or performance__. There's a kind of implied performance in the movement of the tanks in the sense that "good movement" is about positioning yourself to kill and not be killed. The tanks do move kind of gracefully/pleasingly. One could imagine the tanks _dancing_ instead of killing. Could they dance together? (What the fuck does this have to do with Light though? The _Spotlight_ could be an understanding of a staged performance rather than a war?). Music would come into this too (not clear whether you're then doing something that is "more Light" or "more Audio"? Both are central cues to a particular behavioural context). Illuminated "footsteps" that guide the tanks to dance with one another? Just the exhortation to "Dance" at the beginning and the ability to do so? Perhaps a semi-arbitrary points system around dancing (turns and accelerations, maintained proximity to your partner, low variance of distance and rotation as sign of synchronisation, sounds kind of nice?) _Dancing in the Moonlight_? Make the directional light Nighttime and off you go! (A Spotlight perhaps most strongly implies a _Circus_ performance? -- one tank as tamer and one as the lion? Slightly effective shells as the whip? Contact as eating? What the fuck? I mean, it sounds pretty amazing...? _The Lion Tamer in the Spotlight_)

__Shooting__. Aiming and firing a projectile at your opponent is pretty central to TANKS! eh. I've already thought in terms of _Light Shells_ where you fire light, but it feels a little too much like a simple remodeling of a shell to a light, even when it's persistent in the scene. Too much like war still? (It's occurring to me as I write this that there is just _no end_ to the possibilities of design in this project... I'm just going to have to draw a line when I have like 5 strong candidates or something? I may need some arbitrary number. 36? Maybe that's a little strong. 7? 17? 27? 3? la la la.) The idea in which you _are_ light and you _shoot_ light as a part of yourself has a kind of weirder and maybe more interesting take on this? Light has issues around display directionality though so I don't know how easily it would feel to move and aim especially? It somehow still feels a bit dead as an idea? I mean somewhat ironically the shooting is the thing I find the _least_ interesting. _Shooting at the Lights_ is a plausible thing to do. De-illuminating the scene by shooting out spotlights. Why would you actually do this? Yes it could be a stupid game where you try to shoot out your opponent's lights, but that's just a displaced version of the same thing, with spotlights as a health bar and target? You shoot out lights because you don't want to be seen.

__Shooting: As Giving__. A projectile can be something you're giving to someone else on purpose, not to hurt them. Still pretty game-y but non-violent. A game of catch. "Lumen accepe et imperti" was my school's motto - take the light and pass it on. Light as a symbol of _knowledge_ in that case. The idea that you need (for some reason?) to pass the light back and forth as a representation of successful play? Before it runs out?

__Shooting: As a ball game__. A projectile matches well with any number of ball sports. Basketball is an obvious one. (This again has fuck all to do with light, but this broader analysis of the TANKS! design is still important so I'll keep going.) These categories can always serve as templates for the other variation collections.

__Shooting: As "applying"__. You can thinking shooting a projectile as applying the meaning of that projectile to the target. So you could think of something like _Lighting Candles_ say, where you're trying to illuminate the scene by lighting set elements (flames even?). What happens when you illuminate it all? Or are they votive candles memorialising the dead? The graves of Sainted Tanks of the past??? _In Memory Of_. Playing tanks and then solemly lighting a candle to the tank that was killed? (By you?)

__Evaluation__. The other key bit of TANKS! is about evaluating the current state of play (and applying the knowledge to your MOVE and SHOOT verbs). Light clearly plays into that evaluation given that it determines what you see and thus the meaning of the situation as you perceive it. Somehow my mind is latching onto this less immediately, perhaps just because it's not an active verb particularly.

Okay I'm exhausted at this point and I've made vast progress with this document. Time to make some stuff.


# 2018-01-06 16:27, Do not go gentle into that good night

```
Do not go gentle into that good night,
Old age should burn and rave at close of day;
Rage, rage against the dying of the light.

Though wise men at their end know dark is right,
Because their words had forked no lightning they
Do not go gentle into that good night.

Good men, the last wave by, crying how bright
Their frail deeds might have danced in a green bay,
Rage, rage against the dying of the light.

Wild men who caught and sang the sun in flight,
And learn, too late, they grieved it on its way,
Do not go gentle into that good night.

Grave men, near death, who see with blinding sight
Blind eyes could blaze like meteors and be gay,
Rage, rage against the dying of the light.
```

- Dylan Thomas

A game that is just that poem being recited as the scene retreats to total blackness, as the tanks play on try to kill each other to the end? Interstitials? Subtitles? Read aloud? A computer voice? A lightning strike on lightning? Nah. But think about some visual approach to the gradual diminishing and extinguishing of all light in the scene. Sound? Think about it.

---

## 2018-01-25 12:17, In which I rethink Light in light of the new manifesto/direction

Been a long time since I wrote anything here. I want to revisit the question of Light now that I'm trying to think about a specific "topic of conversation" with Unity (about detuning/turning away from violence).

So the discussion with/via the material of Light is kind of: "what can we do about this?" Questions of how to intervene in the experience/trivialisation of violence in a game like Tanks! (and therefore in all games?)

Put this way, it seems like the biggest thing that Light can do in this situation revolves around making it impossible/difficult for the tanks to _see what they are doing_.

I think there's a weird philosophical question here in terms of whether I want to intervene more heavily that just making it hard to see though?

Or is that enough? And WHAT CAN I CALL A GAME THAT IS TRYING TO DO THIS?

I guess to some extent it isn't really Tanks!es in a weird way?

- De-tanks?

Light is hugely about visibility obviously. Returning to the qualities from above...

- Light as defining space (spotlights you can't drive out of?)
- Light as defining any visibility (complete darkness, complete brightness, diminishing light)
- Fog as reduced visibility (complete fog, increasing fog?)
- Light implies time (by being the sun), so one could make time go very slowly (along with the tanks), one could make time go very fast (so the tanks die of old age?)
- Reflectivity can emphasise being alone? A tank surrounded by mirrors is very alone?
- Refraction can make things difficult to see and therefore enact
- Strobing - slow motion...?
- Shadow and light defines things spatially - a need to stay in the shadows/in the light... perhaps at risk of exploding? (So the tanks have to look at each other perhaps from the shadows but can't reach without death - basically Vampire tank)
- Going into the light still makes a lot of sense in terms of a detune (not about preventing violence but about respecting the consequences)

I guess there's a broader question this raises about more abstract relationships to violence? That might be something I need to write about in the main process but

- Disable guns (can't shoot can't kill)
- Disable damage
- Disable motion (can't reach can't kill)
- Make aiming difficult
- Make motion difficult
- Make shells behave in a different way, remove shells altogether
- Make vision difficult or impossible
- Make vision discouraging
- Recontextualise what is being done altogether with the same asset base (change the rules)

I guess there's going to be more right, and that last one if very broad indeed since it tends to operate more at a metaphorical level perhaps? (e.g. _Walk into the Light_ leverages this, as would _A Little Privacy_ for example).

A question: is it my objective to _eliminate all violence_? Or is it to _question/problematise violence_? Or ... what?
