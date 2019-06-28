# WadPacker

A very simple command-line tool which packs all files in the application directory (and all subdirectories) into a Doom .wad file.

Subdirectories named ExMx or MAPxx are added as maps (so a zero-length entry with the name of the map is added before the lumps created from the files).
