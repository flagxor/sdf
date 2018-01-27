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
  .title" This Deck"
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
  *f" 781 bytes in the current encoding"
  *f" Use more stroke to look better!"
  font !
;slide

slide:
  .title" Signed Distance Function"
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
  .title" Questions?"
  font-cr font-cr
  2 50 font-pick
  .f"   Code online at:" font-cr
  .f"     https://github.com/flagxor/sdf" font-cr
;slide

slideshow

