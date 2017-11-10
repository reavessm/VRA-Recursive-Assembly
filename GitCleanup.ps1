#requires -version 2
<#
.SYNOPSIS
    Cleans up the git repository.
.DESCRIPTION
    This script cleans up a git repository by shuffling files into git-lfs and forcing them to be re-tracked.
.NOTES
    Author: Christopher Skeen (skeen@email.sc.edu)

    Copyright (c) 2017 Christopher Skeen

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
#>

#---------------------------------------------------------[Initialisations]--------------------------------------------------------

#Set Error Action to Silently Continue
#$ErrorActionPreference = "SilentlyContinue"

#----------------------------------------------------------[Declarations]----------------------------------------------------------

Param(
	[Parameter(Mandatory=$True,Position=1)]
	[string]$tracklist,
	[Parameter(Mandatory=$True,Position=2)]
	[int]$kblimit
)

Class lfs_file {
	[bool]$tracked
	[bool]$removed
	[bool]$pushed
	[string]$fullname
	[string]$relativename
	[string]$backupname
}


#-----------------------------------------------------------[Functions]------------------------------------------------------------


Function CleanupGit{
	Param(
  	)
  
	Begin{
  		$files = Get-ChildItem -Path . -Recurse | Where-Object {$_.length/1024 -gt $kblimit}
		$track = gc $tracklist
  		$filelist = New-Object System.Collections.Generic.List[lfs_file]
		$workingdir = $pwd
		$backupdir = Join-Path -Path ( get-item $pwd ).parent.fullname -ChildPath GitRecycleBin
                Write-Host "Writing Tracked File Backup To: " $backupdir
                New-Item -Force -ItemType directory -Path $backupdir >$null
  	}
  
  	Process{
    		Try{
			foreach ($file in $files) {
				$temp = New-Object lfs_file
				foreach ($ex in $track) {
					if ($file.name -like $ex) {
                                                Write-Host "Will Track: " $file.name
						$temp.tracked = $True
                                                break;
					}
					else {
						$temp.tracked = $False
					}
                                        $filelist.Add($file) >$null
                                        cp $file.fullname $backupdir >$null
				}
                                foreach ($f in $filelist) {
                                    
                                }
			}
                }
    		Catch{
      			Break
    		}
  	}
  
	End{
    		If($?){

    		}
  	}
}

#------------------------------ Execution -----------------------------------------------

CleanupGit
