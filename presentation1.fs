#! /usr/bin/env gforth 

require simplefont/slides.fs
require simplefont/gimple1.fs
require samples.fs

slide:
  big home
  font-cr font-cr
  s" Signed Distance Functions" center-type
  s" go Forth" center-type
  font-cr normal
  s" Brad Nelson" center-type
  s" January 22, 2017" center-type
;slide

slide:
  .title" This Deck (aside)"
  *f" Using font code I wrote in Nov 2013!"
  *f" Gimple1 -> Dragon1"
  *f" Generalized Slide Vocabulary"
;slide

slide:
  font @ gimple1 font !
  .title" Gimple1"
  5 44 font-pick
  .f"     " 64 32 do i font-emit loop font-cr
  .f"     " 96 64 do i font-emit loop font-cr
  .f"     " 128 96 do i font-emit loop font-cr
  *f" Smallest nice font I could fit"
  *f" 95 visible ASCII characters"
  *f" ~9k as a TrueType font"
  *f" 585 bytes in the current encoding"
  *f" 2 strokes for numbers and lowercase"
  +f" letters"
  *f" no more then 3 strokes in any glyph"
  font !
;slide

slide:
  font @ dragon1 font !
  .title" Dragon1"
  5 44 font-pick
  .f"     " 64 32 do i font-emit loop font-cr
  .f"     " 96 64 do i font-emit loop font-cr
  .f"     " 128 96 do i font-emit loop font-cr
  *f" 95 visible ASCII characters"
  *f" 744 bytes in the current encoding"
  *f" Use more strokes to look better!"
  font !
;slide

: quo [char] " font-emit ;
slide:
  .title" Slide Vocabulary"
  normal
  .f" slide:" font-cr
  .f"   .title" quo .f"  This Deck (aside)" quo font-cr
  .f"   *f" quo .f" Using font code I wrote in Nov 2013!" quo font-cr
  .f"   *f" quo .f" Gimple1 -> Dragon1" quo font-cr
  .f"   *f" quo .f" Generalized Slide Vocabulary" quo font-cr
  .f" ;slide" font-cr
;slide

slide:
  .title" Slide Vocabulary PRO/CON"
  *f" :-) Uses Forth"
  *f" :-) Integrates Generated Graphics"
  *f" :-( Requires Linux"
  *f" :-( No Current Option to Export"
;slide

slide:
  big home
  font-cr font-cr
  s" Main Presentation" center-type
;slide

slide:
  .title" Signed Distance Functions"
  *f" The signed distance to a surface"
  *f" Negative inside"
  *f" Positive outside"
  *f" Zero on the surface"
;slide

slide:
  .title" Use Cases"
  *f" Compact format for a scalable font"
  *f" Ideal for 'ray-marching' rendering"
  *f" Great for Constructive-Solid-Geometry"
;slide

slide:
  .title" Why use with Forth?"
  *f" Simple Representation but Powerful"
  *f" Numerically Simple"
  *f" (doesn't need floats)"
  *f" Work well when resampled"
  *f" Captures normal as gradient"
;slide

slide:
  .title" Sphere"
  *f" Distance from center minus radius"
  *f" : sphere ( r -- d )"
  +f"      x @ square y @ square +"
  +f"      z @ square + sqrt swap - ;"
;slide

slide:
  .title" Sphere"
  ['] sphere-model field-slice
;slide

slide:
  .title" Union"
  *f" : union ( a b -- d ) min ;"
;slide

slide:
  .title" Union"
  ['] union-model field-slice
;slide

slide:
  .title" Intersect"
  *f" : intersect ( a b -- d ) max ;"
;slide

slide:
  .title" Intersect"
  ['] intersect-model field-slice
;slide

slide:
  .title" Inverse"
  *f" : inverted ( a -- d ) negate ;"
;slide

slide:
  .title" Inverse"
  ['] sphere-inverse-model field-slice
;slide

slide:
  .title" Difference"
  *f" : intersect ( a -b -- d ) negate max ;"
;slide

slide:
  .title" Difference"
  ['] difference-model field-slice
;slide

slide:
  .title" Box"
  *f" Absolute distance from each side"
  *f" : box ( x y z -- d )
  +f"      z @ abs swap -"
  +f"      swap y @ abs swap - max"
  +f"      swap x @ abs swap - max ;"
;slide

slide:
  .title" Box"
  ['] box-model field-slice
;slide

slide:
  .title" Cylinder"
  *f" Box tops intersected with"
  +f"     circle distance"
  *f" : cylinder ( h r -- d )"
  +f"    x @ square y @ square + sqrt swap -"
  +f"    swap z @ abs swap - max ;
;slide

slide:
  .title" Cylinder"
  ['] cylinder-model field-slice
;slide

slide:
  .title" Geometry Stack"
  *f" 20 constant stack-slots"
  +f" create gstack"
  +f" stack-slots cells 3 * allot"
  +f" variable gsp gstack gsp !"
  *f" : >g ( n -- ) gsp @ ! cell gsp +! ;"
  +f" : g> ( -- n ) -1 cells gsp +! gsp @ @ ;"
  *f" : gpush   x @ >g y @ >g z @ >g ;"
  +f" : gpop   g> z ! g> y ! g> x ! ;"
;slide

slide:
  .title" Models"
  *f" gpush"
  +f"   128 128 0 translated 80 sphere"
  +f"   gpush"
  +f"     20 forward"
  +f"     gpush"
  +f"       30 left 20 sphere difference"
  +f"     gpop"
  +f"     gpush"
  +f"       30 right 20 sphere difference"
  +f"     gpop"
  +f"   gpop"
  +f"   gpush"
  +f"     30 back 50 10 10 box difference"
  +f"   gpop"
  +f" gpop"
;slide

slide:
  .title" Models"
  ['] smiley-model field-slice
;slide

slide:
  .title" Ray Marching"
  *f" Use SDF to find a distance on a ray"
  *f" Multiple steps vs Ray-Tracing"
  *f" Simpler than Ray-Tracing"
;slide

variable circle-x   variable circle-y   variable circle-r
: cpt ( x y ) circle-r @ * swap circle-r @ * circle-x @ + swap circle-y @ + ;
: circle ( x y r )
    circle-r ! circle-y ! circle-x !
    -1 0 cpt -1 -1 cpt 0 -1 cpt quartic
    0 -1 cpt 1 -1 cpt 1 0 cpt quartic
    1 0 cpt 1 1 cpt 0 1 cpt quartic
    0 1 cpt -1 1 cpt -1 0 cpt quartic
;

slide:
  .title" Ray Marching"
  width 10 20 */ height 10 20 */ width 3 20 */ circle
  width 10 20 */ height 10 20 */
  width 13 20 */ height 10 20 */ line
  width 13 20 */ height 10 20 */ width 2 20 */ circle
  width 13 20 */ height 10 20 */
  width 15 20 */ height 10 20 */ line
  width 15 20 */ height 10 20 */ width 1 20 */ circle
  width 15 20 */ height 10 20 */
  width 16 20 */ height 10 20 */ line
  width 8 20 */ height 16 20 */
  width 19 20 */ height 10 20 */ line
;slide

slide:
  .title" Ray Marching"
  *f" Find surface normals using gradident"
  *f" Measure gradient with small epsilons"
;slide

slide:
  .title" Ray Marching"
  ['] smiley2-model raymarch-model
;slide

slide:
  .title" Future Work"
  *f" Finish Perspective Projection"
  *f" Add Lighting"
;slide

slide:
  .title" Questions?"
  font-cr font-cr
  2 50 font-pick
  .f"   Code online at:" font-cr
  .f"     https://github.com/flagxor/sdf" font-cr
;slide

slideshow

