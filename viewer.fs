#! /usr/bin/env gforth

require simplefont/grf.fs
require sdf.fs
\ require raymarch.fs

256 constant iwidth
256 constant iheight
iwidth iheight * 4 * constant image-size

create disp-image image-size allot

: blt-image
   0 0 pixel
   height 0 do
     width 0 do
       i iwidth width */ j iheight height */
       iwidth * + 4 * disp-image + over 4 cmove
       4 +
     loop
   loop
   drop
;

: draw
   blt-image
   flip
;

: >byte 255 min 0 max ;

: model
  gpush
    128 128 0 translated
    50 sphere
  gpop
  gpush
    180 100 0 translated
    30 30 30 box
  gpop
  difference
;
' model set-model

: field-slice
   disp-image
   iheight 0 do
     iwidth 0 do
       i j 0 set-point
       distance 2* 2* 2*
       dup >r dup >r
       >byte over c! 1+
       r> negate >byte over c! 1+
       r> dup negate >byte swap 0= or over c! 1+
       255 over c! 1+
     loop
   loop
   drop
;

: handle-events
   wait
   event expose-event = if draw then
   event press-event = if
     last-key 13 = if bye then
     last-key [char] a = if field-slice draw then
   then
;

: main
   512 512 window
   begin handle-events again
;
main
