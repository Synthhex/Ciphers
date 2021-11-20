# Ciphers
A collection of various cyphers implemented in C#.

# Transposition cipher
Transposition.cs contains a unique implementation of the cipher. Instead of using an alphabetical keyword, it uses a numerical key and follows the sequential order of the columns. It also does not pad the string up to a multiple of the key, but rather skips over the empty spaces at the encryption and simulates where they would have been at the decryption step.
