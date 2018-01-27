#! /usr/bin/env gforth 

require simplefont/slides.fs

: title
  big home
  font-cr font-cr
  s" Signed Distance Functions" center-type
  s" in Forth" center-type
  font-cr normal
  s" Brad Nelson" center-type
  s" January 22, 2017" center-type
;

: motivation
  .title" Motivation"
  *f" Simple Representation but Powerful"
;

: questions?
  .title" Questions?"
  font-cr font-cr
  2 50 font-pick
  .f"   Code online at:" font-cr
  .f"     https://github.com/flagxor/sdf" font-cr
;

here
' title ,
' motivation ,
' questions? ,
slideshow

