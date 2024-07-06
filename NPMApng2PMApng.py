#    https://github.com/maxme/PNG-Alpha-Premultiplier/blob/master/NPMApng2PMApng.py
#
#    Copyright (C) 2011, Maxime Biais <http://twitter.com/maximeb>
#
#    This program is free software; you can redistribute it and/or modify
#    it under the terms of the GNU General Public License as published by
#    the Free Software Foundation; either version 2 of the License, or
#    (at your option) any later version.
#
#    This program is distributed in the hope that it will be useful,
#    but WITHOUT ANY WARRANTY; without even the implied warranty of
#    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
#    GNU General Public License for more details.
#
#    You should have received a copy of the GNU General Public License
#    along with this program; if not, write to the Free Software
#    Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
#

from PIL import Image
import numpy

def is_pma(matrix):
    for line in matrix:
        for e in line:
            if e[3] != 255:
                if e[0] > e[3] or e[1] > e[3] or e[2] > e[3]:
                    return False
    return True

def main(finput, foutput):
    # load
    #print "load image"
    im = Image.open(finput)
    matrix = numpy.array(im)
    # premultiply alpha   # print statments need to be fixed. they where 13 years old
    print("checking PMA")
    if is_pma(matrix):
        print(f"Skipped {finput}: already Premultiplied Alpha image")
        return
    print("premultiplying matrix...")
    for line in matrix:
        for v in line:
            v[0] = v[0] * (v[3] / 255.)
            v[1] = v[1] * (v[3] / 255.)
            v[2] = v[2] * (v[3] / 255.)
    # save
    #print "matrix to image"
    res = Image.fromarray(matrix)
    #print "export to png"
    res.save(foutput)

if __name__ == "__main__":
    import sys
    # # I replaced the comand line arguments with a automated script
    # main(sys.argv[1], sys.argv[2]) 
    
    import os

    os.chdir(os.path.dirname(__file__))

    os.chdir("normal_sprites")
    folders = os.listdir()

    for each in folders:

        files = os.listdir(each)
        # print(each, files)

        for image in files:

            print(f"normal_sprites/{each}/{image}", f"sprites/{each}/{image}")
            main( f"./{each}/{image}", f"../sprites/{each}/{image}")
    
    # main()



